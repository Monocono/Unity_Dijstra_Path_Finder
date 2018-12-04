using System;

public class PriorityQueue
{
	private FindPath.Vertex[] items;
	private int tail = 0;
	private int capacity;

	public int Count
	{
		get
		{
			return tail;
		}
	}

	public PriorityQueue(int capacity = 4)
	{
		this.capacity = capacity;
		items = new FindPath.Vertex[capacity];
	}

	private void Grow()
	{
		int newCapacity = capacity * 2;
		FindPath.Vertex[] newItems = new FindPath.Vertex[newCapacity];
		Array.Copy(items, newItems, capacity);
		items = newItems;
		capacity = newCapacity;
	}

	public void Add(FindPath.Vertex item)
	{
		if (Count == capacity) Grow();

		item.lastHeapIndex = tail;
		items[tail++] = item;
		SortUp(item);
	}

	public FindPath.Vertex Pop()
	{
		if (Count == 0) throw new InvalidOperationException("PriorityQueue is empty");

		FindPath.Vertex firstItem = items[0];
		items[0] = items[--tail];
		items[0].lastHeapIndex = 0;
		SortDown(items[0]);
		return firstItem;
	}

	public void UpdateItem(FindPath.Vertex item)
	{
		SortUp(item);
	}

	public bool Contains(FindPath.Vertex item)
	{
		if (item.lastHeapIndex >= tail) return false;
		return items[item.lastHeapIndex] == item;
	}

	void SortDown(FindPath.Vertex item)
	{
		while (true)
		{
			int childIndexLeft = item.lastHeapIndex * 2 + 1;
			int childIndexRight = item.lastHeapIndex * 2 + 2;
			int swapIndex = 0;

			if (childIndexLeft < tail)
			{
				swapIndex = childIndexLeft;

				if (childIndexRight < tail)
				{
					if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
					{
						swapIndex = childIndexRight;
					}
				}

				if (item.CompareTo(items[swapIndex]) < 0)
				{
					Swap(item, items[swapIndex]);
				}
				else
				{
					return;
				}

			}
			else
			{
				return;
			}

		}
	}

	private void SortUp(FindPath.Vertex item)
	{
		int parentIndex = (item.lastHeapIndex - 1) / 2;

		while (true)
		{
			FindPath.Vertex parentItem = items[parentIndex];
			if (item.CompareTo(parentItem) > 0)
			{
				Swap(item, parentItem);
			}
			else
			{
				break;
			}

			parentIndex = (item.lastHeapIndex - 1) / 2;
		}
	}

	private void Swap(FindPath.Vertex a, FindPath.Vertex b)
	{
		items[a.lastHeapIndex] = b;
		items[b.lastHeapIndex] = a;
		int itemAIndex = a.lastHeapIndex;
		a.lastHeapIndex = b.lastHeapIndex;
		b.lastHeapIndex = itemAIndex;
	}
}