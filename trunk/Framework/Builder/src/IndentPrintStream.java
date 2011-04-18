import java.io.PrintStream;


public class IndentPrintStream 
{
	private PrintStream out;

	private StringBuilder indents;
	
	private String tab;
	
	private boolean needIndent;
	
	public IndentPrintStream(PrintStream out)
	{
		this.out = out;
		indents = new StringBuilder();
		tab = "\t";
	}
	
	public void setTab(String tab)
	{
		this.tab = tab;
	}
	
	public IndentPrintStream print(Object obj)
	{
		writeIndents();
		out.print(obj);
		return this;
	}
	
	public IndentPrintStream println(Object obj)
	{
		writeIndents();
		out.println(obj);
		needIndent = true;
		return this;
	}
	
	public IndentPrintStream println()
	{
		writeIndents();
		out.println();
		needIndent = true;
		return this;
	}
	
	public IndentPrintStream incTab()
	{
		needIndent = true;
		indents.append(tab);
		return this;
	}
	
	public IndentPrintStream decTab()
	{
		if (indents.length() > 0)
		{
			indents.setLength(indents.length() - tab.length());
		}
		return this;
	}
	
	private void writeIndents() 
	{
		if (needIndent)
		{
			out.print(indents);
			needIndent = false;
		}
	}

	public void close() 
	{
		out.close();
	}
}
