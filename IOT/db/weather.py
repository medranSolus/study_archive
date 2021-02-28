#!/usr/bin/env python3
# -*- coding: utf-8 -*-
import fire
import datetime as datetime
import random


def get_data_points(measurementName, runNo=None):
    # Get the three measurement values from the "sensors"
    temperature = random.randint(20,25)
    pressure = random.randint(1021,1032)
    humidity = random.randint(56,66)
    # Get a local timestamp
    timestamp = datetime.datetime.utcnow().isoformat()
    datapoints = [
            {
                "measurement": measurementName,
                "tags": {"runNum": runNo},
                "time": timestamp,
                "fields": {
                    "temperaturevalue": temperature,
                    "pressurevalue": pressure,
                    "humidityvalue": humidity
                }
            }
    ]
    return datapoints


if __name__ == "__main__":
    fire.Fire(get_data_points)