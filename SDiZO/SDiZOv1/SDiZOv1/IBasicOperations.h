#pragma once
namespace DataTypes
{
	///<summary>
	///Interfejs definiujący podstawowe operacje na strukturach 
	///</summary>
	__interface IBasicOperations
	{
		bool contains(long value) const;
		void show() const;
		void clear();
	};
}