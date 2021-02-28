#include "pch.h"
#include "Queue.h"
#include <utility>

void Queue::resize(long long size)
{
	Edge * newHeap = new Edge[size];
	for (long long i = 0; i < count; ++i)
		newHeap[i] = std::move(heap[i]);
	delete[] heap;
	heap = newHeap;
	count = size;
}

Queue::Queue(long long size) : count(size)
{
	heap = new Edge[count];
	heapPosition = 0;
}

void Queue::push(Edge edge)
{
	if (heapPosition == count)
		resize(count * 1.1 + 10);
	long long i = heapPosition++;
	long long j = (i - 1) >> 1;
	while (i && (heap[j].getValue() > edge.getValue()))
	{
		heap[i] = heap[j];
		i = j;
		j = (i - 1) >> 1;
	}
	heap[i] = edge;
}

void Queue::pop()
{
	if (heapPosition)
	{
		Edge temp = heap[--heapPosition];
		long long i = 0, j = 1;
		while (j < heapPosition)
		{
			if (j + 1 < heapPosition && heap[j + 1].getValue() < heap[j].getValue()) 
				j++;
			if (temp.getValue() <= heap[j].getValue()) 
				break;
			heap[i] = heap[j];
			i = j;
			j = (j << 1) + 1;
		}
		heap[i] = temp;
	}
}