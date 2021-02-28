#pragma once
#include "Edge.h"

class Queue
{
	Edge * heap = nullptr;
	long long heapPosition = 0;
	long long count = 0;

	void resize(long long size);
public:
	Queue(long long size);

	inline Edge front() const { return heap[0]; }
	inline long long size() const { return heapPosition; }

	void push(Edge edge);
	void pop();

	~Queue() { if (heap) delete[] heap; }
};