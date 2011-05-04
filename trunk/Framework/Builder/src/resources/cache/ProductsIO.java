package resources.cache;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.List;
import java.util.Map.Entry;
import java.util.Set;

import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.DocumentHelper;
import org.dom4j.Element;
import org.dom4j.io.OutputFormat;
import org.dom4j.io.SAXReader;
import org.dom4j.io.XMLWriter;

public class ProductsIO
{
	public static void write(Products products, File file) throws IOException
	{
		Document doc = DocumentHelper.createDocument();
		Element root = doc.addElement("products");
		for (Product product : products.getProducts())
		{
			Element productElement = root.addElement("product");
			productElement.addAttribute("name", product.getName());
			File productFile = product.getFile();
			if (productFile != null)
			{
				productElement.addAttribute("path", productFile.getAbsolutePath());
				productElement.addAttribute("lastModified", Long.toString(productFile.lastModified()));
			}
				
			Set<Entry<String, String>> entrySet = product.getProperties().entrySet();
			for (Entry<String, String> e : entrySet)
			{
				Element propertyElement = productElement.addElement("property");
				propertyElement.addAttribute("name", e.getKey());
				propertyElement.addAttribute("value", e.getValue());
			}
		}
		
		writeDoc(file, doc);
	}
	
	private static void writeDoc(File file, Document doc) throws IOException
	{
		System.out.println("Writing document: " + file);
		FileOutputStream stream = null;
		try
		{
			stream = new FileOutputStream(file);
			
			try 
			{
				XMLWriter writer = new XMLWriter(stream, OutputFormat.createPrettyPrint());
				writer.write(doc);
			} 
			catch (UnsupportedEncodingException e) 
			{
				e.printStackTrace();
			}
		}
		finally
		{
			if (stream != null)
				stream.close();
		}
	}
	
	public static Products load(File file)
	{
		if (!file.exists())
		{
			return null;
		}
		
		try
		{
			Document doc = new SAXReader().read(file);
			
			Products products = new Products();
			
			Element root = doc.getRootElement();
			List<Element> productElements = root.elements("product");
			for (Element productElement : productElements)
			{
				String name = productElement.attributeValue("name");
				String path = productElement.attributeValue("path");
				File productFile = null;
				long lastModified = 0L;
				if (path != null)
				{
					productFile = new File(path);
					lastModified = Long.parseLong(productElement.attributeValue("lastModified"));
				}
				Product product = new Product(name, productFile);
				product.setLastModified(lastModified);
				
				List<Element> propertyElements = productElement.elements("property");
				for (Element propertyElement : propertyElements)
				{
					String propName = propertyElement.attributeValue("name");
					String propValue = propertyElement.attributeValue("value");
					product.addProperty(propName, propValue);
				}
				
				products.add(product);
			}
			
			return products;			
		}
		catch (DocumentException e)
		{
		}
		
		return null;
	}
}
