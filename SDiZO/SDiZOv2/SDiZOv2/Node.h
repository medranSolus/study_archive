#pragma once

class Node
{
	Node * next = nullptr;
	long long nextVertex = 0;
	long long weight = 0;
public:
	Node() {}
	Node(long long nextVertex, long long edgeWeight, Node * nextNode = nullptr);

	inline long long getNextVertex() const { return nextVertex; }
	inline long long getWeight() const { return weight; }
	inline Node * getNext() const { return next; }

	inline void setNextVertex(long long nextVertex, Node * nextEdge) { nextVertex = nextVertex; next = nextEdge; }
	inline void setValue(long long edgeValue) { weight = edgeValue; }
};