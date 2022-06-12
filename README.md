# Dynamic-Line-Graph-using-Xamarin

The application displays and saves accelerometer and gyroscope data from a phone.

![alt text](https://github.com/DeAsianOne/Dynamic-Line-Graph-using-Xamarin/blob/main/_Images/App.jpg?raw=true)

- Toggle sensor turns the sensor on and off.
- Accelerometer and gyroscope readings are real time and displayed to two decimal places. This can be changed to a larger number of significant figures in the code.
- Graphs are updated as sensor readings are updated. Top graph is accelerometer and bottom is gyroscope. This graph can be considered 'dynamic' as it moves in accordance to new data. However, it is actually a static graph continuosly deleted and recreated as the sensor data changes/each time it is collected. I could not find a way for the graph to be continous using Xamarin so this is my ingenious solution, this may have been done before but I couldn't find the code anywhere.
- Sensor reading has a frequency of 14Hz.
- Data is saved into external storage somewhere on phone, you may have to do some digging around. 

