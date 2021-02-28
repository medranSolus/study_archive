#pragma once
#include <iostream>
#include "IArray.h"
namespace DataTypes
{
	///<summary>
	///Klasa odpowiedzialna za reprezentacjê listy jednokierunkowej
	///</summary>
	class List : public IArray
	{
		class Node
		{
			Node * nextItem = nullptr;
			long data = 0;
		public:
			Node() {}
			Node(long value) : data(value) {}
			Node(long value, Node * nextNode) : data(value), nextItem(nextNode) {}

			inline Node ** next() { return &nextItem; }
			inline long getData() const { return data; }
			inline long & getData() { return data; }
			inline void setData(long value) { data = value; }

			inline friend std::istream & operator>>(std::istream & is, Node & node) { is >> node.data; return is; }
		};

		long count = 0;
		Node * head = nullptr;
		Node * tail = nullptr;
	public:
		List() {}
		List(const std::string & fileName);

		inline void show() const { std::cout << *this; }

		bool contains(long value) const;
		void popFront();
		void popBack();
		void erase(long value);
		void pushFront(long value);
		void pushBack(long value);
		void insert(long afterValue, long value);
		void clear();

		friend std::ostream & operator<<(std::ostream & os, const List & list);
		friend std::ofstream & operator<<(std::ofstream & of, const List & list);
		friend std::istream & operator>>(std::istream & is, List & list);

		~List();
	};
}