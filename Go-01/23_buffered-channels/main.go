package main

import (
	"fmt"
	"time"
)

// Buffered channel sẽ block goroutine nếu:
// - Gửi dữ liệu vào vượt quá sức chứa
// - Nhận dữ liệu từ channel rỗng

func main() {
	// Tạo buffered channel có sức chứa là 3
	ch := make(chan string, 3)

	fmt.Printf("Channel: length = %d, capacity = %d\n", len(ch), cap(ch))

	// Đưa dữ liệu vào buffered channel
	ch <- "Tí"
	fmt.Printf("Channel: length = %d, capacity = %d\n", len(ch), cap(ch))
	ch <- "Tèo"
	fmt.Printf("Channel: length = %d, capacity = %d\n", len(ch), cap(ch))

	ch <- "Cóc"
	// ch <- "Ổi" // deadlock vì vượt quá sức chứa của channel

	// Lấy dữ liệu ra từ buffered channel
	<-ch
	fmt.Printf("Channel: length = %d, capacity = %d\n", len(ch), cap(ch))

	// Buffered channel hoạt động như một hàng đợi (queue - FIFO)
	numberCh := make(chan int, 5)
	for i := 1; i <= 5; i++ {
		numberCh <- i
	}
	for i := 1; i <= 5; i++ {
		fmt.Printf("%d ", <-numberCh)
	}
	fmt.Println()

	// Khai báo unbuffered channel
	// unbufferedCh := make(chan int)
	// unbufferedCh <- 1 // deadlock

	// Khai báo buffered channel
	bufferedCh := make(chan int, 1)
	bufferedCh <- 1

	numCh := make(chan int, 2)
	go sendNumber(numCh)
	time.Sleep(2 * time.Second)
	for n := range numCh {
		fmt.Printf("Value from channel %d\n", n)
		time.Sleep(2 * time.Second)
	}
}

func sendNumber(ch chan int) {
	for i := 1; i <= 5; i++ {
		ch <- i
		fmt.Printf("Send %d to channel\n", i)
	}
	close(ch)
}
