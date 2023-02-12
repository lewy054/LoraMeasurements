# LoraMeasurements

## Table of contents
* [General info](#general-info)
* [Apperance](#apperance)
* [Technologies](#technologies)
* [Setup](#setup)

## General info
LoraMeasurements is a web application developed using ASP.NET CORE for the backend and Vue for the frontend. The purpose of this application is to receive weather data from an IoT device (e.g. Arduino) over LoRaWAN via webhooks.

## Apperance
### List of devices
![Imgur Image](https://i.imgur.com/leC8idG.png)

### Device view with temperature, relative humidity and pressure information
![Imgur Image](https://i.imgur.com/Pgzwqna.png)
![Imgur Image](https://i.imgur.com/Zroek5N.png)

### Device view with device information including voltage, channel RSSI, RSSI, SNR and which channels were used
![Imgur Image](https://i.imgur.com/om2bujr.png)
![Imgur Image](https://i.imgur.com/67C0pVx.png)

### Map view showing device position
![Imgur Image](https://i.imgur.com/VzFIMHk.png)


## Technologies
Project is created with:
* C#
* ASP.NET CORE
* MSSQL
* Vue
* Vuetify
* Vue Router
* Vue3-OpenLayers
* Pinia
* Typescript
* HTML
* CSS
* ChartsJs

## Setup
### Add a webhook
To add a webhook add your device to [The Things of Network](https://www.thethingsnetwork.org/), and add new webhook pointing to http://\<url\>/ttn

