package main

import "fmt"

func main() {
	// if statement
	a := 8
	if a%2 == 0 {
		fmt.Printf("Số %d là số chẵn\n", a)
	}

	// if-else statement
	b := 11
	if b%2 == 0 {
		fmt.Printf("Số %d là số chẵn\n", b)
	} else {
		fmt.Printf("Số %d là số lẻ\n", b)
	}

	// if-else if-else
	c := 80
	if c <= 50 {
		fmt.Printf("Số %d nhỏ hơn hoặc bằng 50\n", b)
	} else if c <= 100 {
		fmt.Printf("Số %d lớn hơn 50 và nhỏ hơn hoặc bằng 100\n", b)
	} else {
		fmt.Printf("Số %d lớn hơn lớn hơn 100\n", b)
	}

	// n := sum(8, 15)

	// Khai báo lệnh gán trong if sẽ giúp giảm tầm vực của biến, nó chỉ có thể được truy suất bên trong block if-else
	if n := sum(8, 15); n%2 == 0 {
		fmt.Printf("Số %d là số chẵn\n", n)
	} else {
		fmt.Printf("Số %d là số lẻ\n", n)
	}

	if n := sum(9, 17); n%2 == 0 {
		fmt.Printf("Số %d là số chẵn\n", n)
	} else {
		fmt.Printf("Số %d là số lẻ\n", n)
	}

	// fmt.Println("n", n)
}

func sum(a, b int) int {
	return a + b
}
