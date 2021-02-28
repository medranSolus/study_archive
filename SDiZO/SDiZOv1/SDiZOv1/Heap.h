#pragma once
#include <iostream>
#include "ITree.h"
namespace DataTypes
{
	///<summary>
	///Klasa odpowiedzialna za reprezentacjê kopca binarnego
	///</summary>
	class Heap : public ITree
	{
		long * data = nullptr;
		long count = 0;

		bool searchNode(const long & value, long index) const;
		void sortUp(long index);
		void sortDown(long index);
		void printHeap(long index, unsigned long depth, std::string & levelsTrace) const;
	public:
		Heap() {}
		Heap(const std::string & fileName);

		inline long size() const { return count; }

		bool contains(long value) const;
		void show() const;
		void clear();
		void add(long value);
		void erase(long key);

		long operator[](long index) const;
		long & operator[](long index);
		friend std::ostream & operator<<(std::ostream & os, const Heap & heap);
		friend std::ofstream & operator<<(std::ofstream & of, const Heap & heap);
		friend std::istream & operator>>(std::istream & is, Heap & heap);

		~Heap();
	};
}