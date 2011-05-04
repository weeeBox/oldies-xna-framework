package tasks;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.apache.tools.ant.BuildException;
import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.Element;
import org.dom4j.io.OutputFormat;
import org.dom4j.io.SAXReader;
import org.dom4j.io.XMLWriter;

import resources.ContentPair;
import resources.ResourceBase;
import resources.ResourceReg;

public class ContentProjSync 
{
	private List<ResourceBase> resources;
	
	public ContentProjSync()
	{
		resources = new ArrayList<ResourceBase>();
	}
	
	public void addResource(ResourceBase res)
	{
		resources.add(res);
	}
	
	public void sync(File projFile)
	{
		try 
		{
			Document doc = new SAXReader().read(projFile);
			processContentProj(doc, projFile);
		} 
		catch (DocumentException e) 
		{
			e.printStackTrace();
			throw new BuildException(e.getMessage());
		}
	}
	
	private void processContentProj(Document doc, File projFile)
	{	
		if (resources.size() > 0)
		{
			clearOldItems(doc);
			addNewItems(doc);
			writeDocument(doc, projFile);
		}
	}

	private void clearOldItems(Document doc) 
	{
		List<ContentPair> contentPairs = ResourceReg.getContentPairs();
		List<Element> itemGroups = doc.getRootElement().elements("ItemGroup");
		for (Element e : itemGroups) 
		{
			List<Element> children = e.elements();
			for (Element child : children) 
			{
				if (child.getName().equals("Compile"))
				{
					Element importerElement = child.element("Importer");
					Element processorElement = child.element("Processor");
					if (importerElement != null)
					{	
						String importer = importerElement.getText();
						String processor = processorElement == null ? null : processorElement.getText();
						ContentPair pair = new ContentPair(importer, processor);
						if (contentPairs.contains(pair))
						{
							String name = child.elementText("Name");
							String filename = child.attributeValue("Include");
							
							if (containsResource(name, filename))
							{
								e.remove(child);
							}
						}
					}					
				}
			}
			if (e.elements().isEmpty())
				e.getParent().remove(e);
		}
	}
	
	private boolean containsResource(String name, String filename)
	{
		for (ResourceBase res : resources)
		{
			if (res.getShortName().equals(name) && res.getDestFile().getName().equals(filename))
				return true;
		}
		
		return false;
	}
	
	private void addNewItems(Document doc) 
	{
		Element parent = doc.getRootElement().addElement("ItemGroup");
		for (ResourceBase res : resources) 
		{
			addResource(res, parent);
		}
	}

	private void addResource(ResourceBase res, Element parent) 
	{
		Element element = parent.addElement("Compile");
		element.addAttribute("Include", res.getDestFile().getName());
		element.addElement("Name").addText(res.getShortName());
		element.addElement("Importer").addText(res.getImporter());
		String processor = res.getProcessor();
		if (processor != null)
		{
			element.addElement("Processor").addText(processor);
		}
	}

	private void writeDocument(Document doc, File file)
	{
		try 
		{
			FileOutputStream stream = new FileOutputStream(file);
			
			OutputFormat format = OutputFormat.createPrettyPrint();
			XMLWriter writer = new XMLWriter(stream, format);
			writer.write(doc);
			writer.flush();
			
			stream.close();
			writer.close();
		} 
		catch (IOException e) 
		{
			e.printStackTrace();
		}
	}
}
