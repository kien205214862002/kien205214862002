package main

import (
	"fmt"
)

func hello(done chan bool) {
	fmt.Println("Hello Gopher")
	done <- true // Gửi dữ liệu vào channel
}

// Định nghĩa func dùng để nhận dữ liệu từ channel: <-chan
func receiveOnly(c <-chan int) {
	fmt.Println("Received:", <-c)

	// c <- 10 // error
}

// Định nghĩa func dùng để gửi dữ liệu vào channel: chan<-
func sendOnly(c chan<- int) {
	c <- 10

	// fmt.Println("Received:", <-c) // error
}

func producer(ch chan int) {
	for i := 1; i <= 10; i++ {
		ch <- i
	}
	close(ch) // Khai báo đóng channel
}

func main() {
	// Khởi tạo channel
	done := make(chan bool)
	go hello(done)

	<-done // Nhận dữ liệu từ channel, chương trình sẽ bị block tại đây cho đến khi nhận được dữ liệu từ channel

	fmt.Println("Main")

	ch := make(chan int)
	go sendOnly(ch)
	fmt.Println("Nhận dữ liệu từ channel:", <-ch)

	go producer(ch)
	// Sử dụng vòng lặp vô tận để nhận dữ liệu từ channel
	// for {
	// 	// biến ok dùng để xác định channel đã đóng hay chưa, ok = true nghĩa là chưa đóng
	// 	n, ok := <-ch

	// 	fmt.Println("Theo dõi và nhận dữ liệu từ channel:", n, ok)

	// 	if ok == false {
	// 		break
	// 	}
	// }

	// Sử dụng for-range để nhận dữ liệu từ channel
	for n := range ch {
		fmt.Println("Theo dõi và nhận dữ liệu từ channel:", n)
	}
}
