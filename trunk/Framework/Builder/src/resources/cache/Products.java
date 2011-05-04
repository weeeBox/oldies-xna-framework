package resources.cache;

import java.util.ArrayList;
import java.util.List;

public class Products
{
	private List<Product> products = new ArrayList<Product>();
	
	public void add(Product product)
	{
		products.add(product);
	}
	
	public List<Product> getProducts()
	{
		return products;
	}

	@Override
	public boolean equals(Object obj)
	{
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Products other = (Products) obj;
		if (!products.equals(other.products))
			return false;
		return true;
	}
}
