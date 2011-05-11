package resources;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import product.Product;
import product.ProductClass;

import tasks.ContentProjTask;

public class PackageRes extends ProductClass
{
	private List<ResourceBase> resources = new ArrayList<ResourceBase>();
	
	private String name;

	public PackageRes()
	{
	}
	
	public PackageRes(String name)
	{
		this.name = name;
	}
	
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
		String productName = name;
		File productsDir = new File(ContentProjTask.resDir, productName);
		productsDir.mkdir();
		
		setProductFile(productsDir, name);
		loadProductsCache();
		
		Product product = new Product(name);
		product.addAttribute("count", resources.size());
		for (ResourceBase res : resources)
		{
			product.addNode("resource")
				   .addAttribute("name", res.getName())
				   .addAttribute("product", productName + "/" + makeProductName(res.getName()) );
		}		
		setProduct(product);
		
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
	
	public void addPixelFont(BitmapFontRes font)
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
