package resources;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import resources.cache.Product;
import resources.cache.ProductClass;
import tasks.ContentProjTask;

public class PackageRes extends ProductClass
{
	private List<ResourceBase> resources = new ArrayList<ResourceBase>();
	
	private String name;
	
	public String getName() 
	{
		return name;
	}

	public void setName(String name) 
	{
		this.name = name;
	}
	
	public void process()
	{
		File productsDir = new File(ContentProjTask.resDir, name);
		productsDir.mkdir();
		
		setProductsFile(new File(productsDir, name + ".xml"));
		loadProductsCache();
		
		Product product = new Product(name);
		addProduct(product);
		
		if (sourceChanged())
		{
			System.out.println("Source changed: " + getName());
			
			List<ResourceBase> resourcesCopy = new ArrayList<ResourceBase>(resources.size());
			resourcesCopy.addAll(resources);
			
			for (ResourceBase resource : resourcesCopy)
			{				
				System.out.println("Process: " + resource);
				resource.process();
			}
			
			saveProductsCache();
		}
	}

	public void addImage(ImageRes image)
	{
		addResource(image);
	}
	
	public void addAtlas(AtlasRes atlas)
	{
		addResource(atlas);
	}
	
	public void addPixelFont(PixelFontRes font)
	{
		addResource(font);
	}
	
	public void addVectorFont(VectorFontRes font)
	{
		addResource(font);
	}
	
	public void addSound(SoundRes sound)
	{
		addResource(sound);
	}
	
	public void addSong(SongRes song)
	{
		addResource(song);
	}
	
	public void addAnim(AnimationRes ani)
	{
		addResource(ani);
	}
	
	private void addResource(ResourceBase res)
	{
		res.setPackage(this);
		resources.add(res);
	}
	
	public List<ResourceBase> getResources()
	{
		return resources;
	}
	
	@Override
	public String toString() 
	{
		StringBuilder buf = new StringBuilder();
		buf.append("Package: " + name );
		for (ResourceBase res : resources) 
		{
			buf.append("\n\t" + res);
		}
		return buf.toString();
	}
}
