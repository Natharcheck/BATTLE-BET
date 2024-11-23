using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
	private T Prefab { get; }
	private bool AutoExpand { get; set; }
	
	private Transform Container { get; }
	
	private List<T> pool;
	
	public PoolMono(T prefab, int count)
	{
		Prefab = prefab;
		Container = null;
		AutoExpand = true;
		
		CreatePool(count);
	}
	
	public PoolMono(T prefab, int count, bool autoExpand)
	{
		Prefab = prefab;
		Container = null;
		AutoExpand = autoExpand;
		
		CreatePool(count);
	}
	
	public PoolMono(T prefab, int count, Transform container)
	{
		Prefab = prefab;
		Container = container;
		AutoExpand = true;
		
		CreatePool(count);
	}
	
	public PoolMono(T prefab, int count, bool autoExpand, Transform container)
	{
		Prefab = prefab;
		Container = container;
		AutoExpand = autoExpand;
		
		CreatePool(count);
	}
	
	private void CreatePool(int count)
	{
		pool = new List<T>();
		
		for (int i = 0; i < count; i++)
			CreateObject();
	}
	
	private T CreateObject (bool isActiveByDefault = false)
	{
		var createdObject = UnityEngine.Object.Instantiate(Prefab, Container);
			createdObject.gameObject.SetActive(isActiveByDefault);
			
		pool.Add(createdObject);
		
		return createdObject;
	}
	
	private bool HasFreeElement(out T freeElement)
	{
		foreach (var element in pool)
		{
			if(!element.gameObject.activeInHierarchy)
			{
				freeElement = element;
				element.gameObject.SetActive(true);
				return true;
			}
		}
		
		freeElement = null;
		return false;
	}
	
	public T GetFreeElement()
	{
		if(HasFreeElement(out var freeElement))
			return freeElement;

		if (AutoExpand)
			return CreateObject(true);
			
		throw new Exception($"No free elements of type {typeof(T)}");
	}
}