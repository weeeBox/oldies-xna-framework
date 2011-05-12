package product;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.List;
import java.util.Map.Entry;
import java.util.Set;

import org.dom4j.Attribute;
import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.DocumentHelper;
import org.dom4j.Element;
import org.dom4j.io.OutputFormat;
import org.dom4j.io.SAXReader;
import org.dom4j.io.XMLWriter;

public class ProductIO
{
	public static void write(Product product, File file) throws IOException
	{
		Document doc = DocumentHelper.createDocument();
		
		ProductNode productRoot = product.rootNode();
		setProductElement(doc.addElement(productRoot.getName()), productRoot);
		
		writeDoc(file, doc);
	}

	private static void setProductElement(Element element, ProductNode productElement)
	{
		Set<Entry<String, String>> attributes = productElement.attributes().entrySet();
		for (Entry<String, String> attr : attributes)
		{
			element.addAttribute(attr.getKey(), attr.getValue());
		}
		
		List<ProductNode> elements = productElement.nodes();
		for (ProductNode productChildElement : elements)
		{
			setProductElement(element.addElement(productChildElement.getName()), productChildElement);
		}
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
	
	public static Product load(File file)
	{
		if (!file.exists())
		{
			return null;
		}
		
		Document doc = null;
		try
		{
			doc = new SAXReader().read(file);
		}
		catch (DocumentException e)
		{
			return null;
		}
		
		Element root = doc.getRootElement();		
		Product product = new Product(root.getName());
		getProductElement(root, product.rootNode());
		return product;			
	}

	private static void getProductElement(Element element, ProductNode productElement)
	{
		List<Attribute> attributes = element.attributes();
		for (Attribute attribute : attributes)
		{
			productElement.addAttribute(attribute.getName(), attribute.getValue());
		}
		
		List<Element> elements = element.elements();
		for (Element child : elements)
		{
			ProductNode productChildElement = productElement.addNode(child.getName());
			getProductElement(child, productChildElement);
		}
	}
}
