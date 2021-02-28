#include "pch.h"
#include "Node.h"

Node::Node(long long nextVertex, long long edgeWeight, Node * nextNode) : nextVertex(nextVertex), weight(edgeWeight), next(nextNode) {}