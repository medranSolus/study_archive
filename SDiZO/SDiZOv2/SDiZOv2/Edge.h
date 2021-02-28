#pragma once

class Edge
{
	long long baseVertex = 0;
	long long nextVertex = 0;
	long long value = 0;
	long long flow = 0;
	Edge * next = nullptr;
public:
	Edge() {}
	Edge(long long nextVertex, long long edgeValue, Edge * nextEdge = nullptr);
	Edge(long long startVertex, long long nextVertex, long long edgeValue, Edge * nextEdge = nullptr);
	Edge(long long edgeFlow, long long startVertex, long long nextVertex, long long edgeValue, Edge * nextEdge = nullptr);

	inline long long getStartVertex() const { return baseVertex; }
	inline long long getNextVertex() const { return nextVertex; }
	inline long long getValue() const { return value; }
	inline long long getFlow() const { return flow; }
	inline Edge * getNext() const { return next; }

	inline void setStartVertex(long long currentVertex) { baseVertex = currentVertex; }
	inline void setNextVertex(long long nextVertex, Edge * nextEdge) { this->nextVertex = nextVertex; next = nextEdge; }
	inline void setValue(long long edgeValue) { value = edgeValue; }
	inline void setFlow(long long edgeFlow) { flow = edgeFlow; }
};