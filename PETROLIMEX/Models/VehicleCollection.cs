﻿using PETROLIMEX;
using System.Collections;

namespace iPGSTools.Models
{
    public class VehicleCollection : CollectionBase
    {
        #region Properties
        public static object lockObj = new object();
        #endregion
        //GET
        public Vehicle this[int index] => (Vehicle)InnerList[index];

        public void UpdateVehicle(string plateNumber, string updatedField1, string updatedField2)
        {
            lock (lockObj)
            {
                Vehicle vehicleToUpdate = GetVehicleByPlate(plateNumber);
                if (vehicleToUpdate != null)
                {
                    // Cập nhật các trường thông tin trong vehicle
                    vehicleToUpdate.beaptransid = updatedField1;
                    vehicleToUpdate.feapresponseid = updatedField2;
                    // Lưu lại vehicle đã được cập nhật vào InnerList
                    InnerList[InnerList.IndexOf(vehicleToUpdate)] = vehicleToUpdate;
                }
            }
        }
        public Vehicle GetVehicleByPlate(string plateNumber)
        {
            lock (lockObj)
            {
                foreach (Vehicle vehicle in InnerList)
                {
                    if (vehicle.platenumber == plateNumber)
                    {
                        return vehicle;
                    }
                }
                return default;
            }
        }

        // Add
        public void Add(Vehicle data)
        {
            lock (lockObj)
            {
                InnerList.Add(data);
            }
        }

        // Remove
        public void Remove(Vehicle data)
        {
            lock (lockObj)
            {
                InnerList.Remove(data);
            }
        }
        public void RemoveById(string plate)
        {
            lock (lockObj)
            {
                foreach (Vehicle vehicle in InnerList)
                {
                    if (vehicle.platenumber == plate)
                    {
                        InnerList.Remove(vehicle);
                        return;
                    }
                }
            }
        }
        public bool IsContainPlate(string plate)
        {
            lock (lockObj)
            {
                foreach (Vehicle vehicle in InnerList)
                {
                    if (StaticPool.StandardlizePlateNumber(vehicle.platenumber) == plate)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public bool IsContainPumpID(string pumpID)
        {
            lock (lockObj)
            {
                foreach (Vehicle vehicle in InnerList)
                {
                    if (vehicle.IDPumping == pumpID)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}