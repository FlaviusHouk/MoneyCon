package md588c6defbe7125f1ad72a98923651efff;


public class AddRecAct_TagSpinnerWrapper
	extends android.widget.TextView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_toString:()Ljava/lang/String;:GetToStringHandler\n" +
			"";
		mono.android.Runtime.register ("App2.Resources.AddRecAct+TagSpinnerWrapper, Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AddRecAct_TagSpinnerWrapper.class, __md_methods);
	}


	public AddRecAct_TagSpinnerWrapper (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == AddRecAct_TagSpinnerWrapper.class)
			mono.android.TypeManager.Activate ("App2.Resources.AddRecAct+TagSpinnerWrapper, Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public AddRecAct_TagSpinnerWrapper (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == AddRecAct_TagSpinnerWrapper.class)
			mono.android.TypeManager.Activate ("App2.Resources.AddRecAct+TagSpinnerWrapper, Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public AddRecAct_TagSpinnerWrapper (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == AddRecAct_TagSpinnerWrapper.class)
			mono.android.TypeManager.Activate ("App2.Resources.AddRecAct+TagSpinnerWrapper, Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public java.lang.String toString ()
	{
		return n_toString ();
	}

	private native java.lang.String n_toString ();

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
