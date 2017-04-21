package md588c6defbe7125f1ad72a98923651efff;


public class NormalSpinner
	extends android.widget.Spinner
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_setSelection:(IZ)V:GetSetSelection_IZHandler\n" +
			"n_setSelection:(I)V:GetSetSelection_IHandler\n" +
			"";
		mono.android.Runtime.register ("App2.Resources.NormalSpinner, Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", NormalSpinner.class, __md_methods);
	}


	public NormalSpinner (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == NormalSpinner.class)
			mono.android.TypeManager.Activate ("App2.Resources.NormalSpinner, Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public NormalSpinner (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == NormalSpinner.class)
			mono.android.TypeManager.Activate ("App2.Resources.NormalSpinner, Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public NormalSpinner (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == NormalSpinner.class)
			mono.android.TypeManager.Activate ("App2.Resources.NormalSpinner, Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void setSelection (int p0, boolean p1)
	{
		n_setSelection (p0, p1);
	}

	private native void n_setSelection (int p0, boolean p1);


	public void setSelection (int p0)
	{
		n_setSelection (p0);
	}

	private native void n_setSelection (int p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
