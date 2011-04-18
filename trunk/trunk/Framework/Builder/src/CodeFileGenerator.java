import java.io.File;
import java.io.IOException;
import java.io.PrintStream;
import java.util.List;

public class CodeFileGenerator 
{
	public void generate(File file, List<Package> packs) throws IOException
	{
		IndentPrintStream out = null;
		try 
		{
			out = new IndentPrintStream(new PrintStream(file));
			writeHeader(out);
			writeCode(out, packs);
			writeEnding(out);
			System.out.println("File created: " + file);
		} 
		finally 
		{
			if (out != null)
				out.close();
		}
	}

	private void writeHeader(IndentPrintStream out) throws IOException 
	{
		out.println("// This file was generated. Do not modify.");
		out.println();
		out.println("using asap.resources;");
		out.println();
		out.println("namespace app");
		out.println("{").incTab();
	}

	private void writeCode(IndentPrintStream out, List<Package> packs) throws IOException
	{
		writePacksIds(out, packs);
		out.println();
		writeResIds(out, packs);
		out.println();
		writeResInfos(out, packs);
	}
	
	private void writePacksIds(IndentPrintStream out, List<Package> packs) 
	{
		out.println("public class ResPacks");
		out.println("{").incTab();
		
		int packIndex;
		for (packIndex = 0; packIndex < packs.size(); packIndex++) 
		{			
			String packName = packs.get(packIndex).getName().toUpperCase();
			out.println("public const int " + packName + " = " + packIndex + ";");
		}
		out.println("public const int PACKS_COUNT = " + packIndex + ";");
		
		out.decTab().println("}");
	}

	private void writeResIds(IndentPrintStream out, List<Package> packs) 
	{
		out.println("public class Res");
		out.println("{").incTab();
		
		int resIndex = 0;
		for (Package pack : packs) 
		{
			out.println("// " + pack.getName().toUpperCase());
			
			List<Resource> packResources = pack.getResources();
			for (Resource res : packResources) 
			{
				out.println("public const int " + res.getLongName() + " = " + resIndex++ + ";");
				if (res instanceof AtlasResource)
				{
					AtlasResource atlas = (AtlasResource) res;
					List<Resource> childRes = atlas.resources();
					for (Resource child : childRes) 
					{
						if (child instanceof Image)
						{
							out.println("public const int " + child.getLongName() + " = " + resIndex++ + ";");
						}
					}
				}
			}
		}
		out.println("public const int RES_COUNT = " + resIndex + ";");
		
		out.decTab().println("}");
	}

	private void writeResInfos(IndentPrintStream out, List<Package> packs) 
	{
		out.println("public class Resources");
		out.println("{").incTab();
		out.println("public static ResourceLoadInfo[][] PACKS =");
		out.println("{").incTab();
		
		for (Package pack : packs) 
		{
			writePackInfo(out, pack);
		}
		
		out.decTab().println("};");
		out.decTab().println("}");
	}

	private void writePackInfo(IndentPrintStream out, Package pack) 
	{
		out.println("// " + pack.getName().toUpperCase());
		out.println("new ResourceLoadInfo[]");
		out.println("{").incTab();
		
		List<Resource> packResources = pack.getResources();
		for (Resource res : packResources) 
		{
			writeResInfo(out, res);
		}
		out.decTab().println("},");
	}

	private void writeResInfo(IndentPrintStream out, Resource res) 
	{
		out.println(String.format("new ResourceLoadInfo(\"%s\", Res.%s, ResType.%s),", res.getShortName(), res.getLongName(), res.getResourceType()));
	}

	private void writeEnding(IndentPrintStream out) throws IOException
	{
		out.decTab().println("}");
	}
}
