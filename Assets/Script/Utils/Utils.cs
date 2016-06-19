using System;


public class Utils
{
	public static T RandomEnumValue<T> ()
	{
		var v = Enum.GetValues (typeof(T));
		return (T)v.GetValue (new Random ().Next (v.Length));
	}
}

