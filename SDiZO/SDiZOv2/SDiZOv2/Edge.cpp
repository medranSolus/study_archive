#include "pch.h"
#include "Edge.h"

Edge::Edge(long long nextVertex, long long edgeValue, Edge * nextEdge) : nextVertex(nextVertex), value(edgeValue), next(nextEdge) {}

Edge::Edge(long long startVertex, long long nextVertex, long long edgeValue, Edge * nextEdge) : Edge(nextVertex, edgeValue, nextEdge) { baseVertex = startVertex; }

Edge::Edge(long long edgeFlow, long long startVertex, long long nextVertex, long long edgeValue, Edge * nextEdge) : Edge(startVertex, nextVertex, edgeValue, nextEdge) { flow = edgeFlow; }