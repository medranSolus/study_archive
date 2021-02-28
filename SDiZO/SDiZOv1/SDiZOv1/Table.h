#pragma once
#include <iostream>
#include "IArray.h"
namespace DataTypes
{
	///<summary>
	///Klasa odpowiedzialna za reprezentacjê tablicy
	///</summary>
	class Table : public IArray
	{
		long * data = nullptr;
		long count = 0;
	public:
		Table() {}
		Table(const std::string & fileName);

		inline long size() const { return count; }
		inline void show() const { std::cout << *this; }

		bool contains(long value) const;
		void clear();
		void popFront();
		void popBack();
		void erase(long index);
		void pushFront(long value);
		void pushBack(long value);
		void insert(long index, long value);

		long operator[](long index) const;
		long & operator[](long index);
		friend std::ostream & operator<<(std::ostream & os, const Table & table);
		friend std::ofstream & operator<<(std::ofstream & of, const Table & table);
		friend std::istream & operator>>(std::istream & is, Table & table);

		~Table();
	};
}