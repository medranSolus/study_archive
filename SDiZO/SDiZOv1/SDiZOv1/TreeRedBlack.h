#pragma once
#include <iostream>
#include <Windows.h>
#include "ITree.h"
namespace DataTypes
{
	///<summary>
	///Klasa odpowiedzialna za reprezentacjê drzewa czerwono-czarnego
	///</summary>
	class TreeRedBlack : public ITree
	{
		enum Color { Red, Black };
		class Node
		{
			long key = 0;
			Color color = Black;
			Node * parent = nullptr;
			Node * childLeft = nullptr;
			Node * childRight = nullptr;
		public:
			Node(long newKey = -1, Color nodeColor = Black, Node * parentNode = &sentinel , Node * leftChild = &sentinel, Node * rightChild = &sentinel);
			
			inline long getKey() const { return key; }
			inline Color getColor() const { return color; }
			inline Node * getParent() const { return parent; }
			inline Node * getLeftChild() const { return childLeft; }
			inline Node * getRightChild() const { return childRight; }

			inline void setKey(long newKey) { key = newKey; }
			inline void setColor(Color nodeColor) { color = nodeColor; }
			inline void setParent(Node * parentNode) { parent = parentNode; }
			inline void setLeftChild(Node * leftChild) { childLeft = leftChild; }
			inline void setRightChild(Node * rightChild) { childRight = rightChild; }
		};

		static std::string frameRight, frameLeft, frameParent;
		static HANDLE consoleHandle;
		static Node sentinel;
		Node * root = &sentinel;

		inline void setDisplayColor(Color color) const { SetConsoleTextAttribute(consoleHandle, 12 - color); }

		void printTree(std::string frameTrace, std::string toDisplay, Node * node) const;
		void purge(Node * node);
		void rotateLeft(Node * node);
		void rotateRight(Node * node);
		void erase(Node * node);
		Node * minimalNode(Node * rootNode) const;
		Node * getSuccessor(Node * rootNode) const;
		Node * getNode(long key);
		Node * searchKey(Node * node, long key);
		bool containsKey(Node * node, long key) const;
	public:
		TreeRedBlack() {}
		TreeRedBlack(const std::string & fileName);

		inline void erase(long key) { erase(getNode(key)); }

		bool contains(long key) const;
		void show() const;
		void clear();
		void add(long key);

		~TreeRedBlack();
	};
}