package md5c88bf0cbedab0fd81c7da62c57a23863;


public class AssignJobP4Activity
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
		mono.android.Runtime.register ("PWCApp.AssignJobP4Activity, PWCApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AssignJobP4Activity.class, __md_methods);
	}


	public AssignJobP4Activity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == AssignJobP4Activity.class)
			mono.android.TypeManager.Activate ("PWCApp.AssignJobP4Activity, PWCApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
