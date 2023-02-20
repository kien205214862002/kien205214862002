package main

import (
	"fmt"
)

// number: 123
func digits(number int, digitCh chan int) {
	for number != 0 {
		digit := number % 10
		digitCh <- digit
		number /= 10
	}
	// Đóng channel sau khi đã tách hết tất cả các số
	close(digitCh)
}

// number = 123 => (1 * 1) + (2 * 2) + (3 * 3)
func calcSquares(number int, squareCh chan int) {
	sum := 0

	// for number != 0 {
	// 	d := number % 10
	// 	sum += d * d
	// 	number /= 10
	// }

	digitCh := make(chan int)
	go digits(number, digitCh)
	for d := range digitCh {
		sum += d * d
	}

	squareCh <- sum
}

// number = 123 => (1 * 1 * 1) + (2 * 2 * 2) + (3 * 3 * 3)
func calcCubes(number int, cubeCh chan int) {
	sum := 0
	// for number != 0 {
	// 	d := number % 10
	// 	sum += d * d * d
	// 	number /= 10
	// }

	digitCh := make(chan int)
	go digits(number, digitCh)
	for d := range digitCh {
		sum += d * d * d
	}

	cubeCh <- sum
}

func main() {
	n := 123

	squareCh := make(chan int)
	cubeCh := make(chan int)

	go calcSquares(n, squareCh)
	go calcCubes(n, cubeCh)

	s, c := <-squareCh, <-cubeCh // block cho tới khi cả 2 channels đều nhận đc dữ liệu từ goroutines

	fmt.Println("Output:", s+c)
}
