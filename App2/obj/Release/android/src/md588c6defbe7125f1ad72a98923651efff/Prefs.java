package md588c6defbe7125f1ad72a98923651efff;


public class Prefs
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("App2.Resources.Prefs, Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Prefs.class, __md_methods);
	}


	public Prefs () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Prefs.class)
			mono.android.TypeManager.Activate ("App2.Resources.Prefs, Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
