package main

import (
	"fmt"
	"sync"
	"time"
)

// defer: cho phép một câu lệnh được gọi ra nhưng sẽ không được thực thi cho đến khi hàm bọc câu lệnh defer return

func main() {
	defer fmt.Println("World!!!")
	fmt.Println("Hello")

	printNumber()

	a := 1
	defer printInt(a)
	a = 10
	fmt.Println("Value trước khi func printInt được gọi", a)

	wg := sync.WaitGroup{}
	for i := 1; i <= 5; i++ {
		wg.Add(1)
		go worker(i, &wg)
	}

	wg.Wait()

	fmt.Println("Main finished")
}

// Các lệnh defer sẽ được đưa vào một stack(LIFO), lệnh defer nào vào sau thì sẽ được thực thi trước
func printNumber() {
	for i := 1; i <= 10; i++ {
		// Giá trị của biến khi gọi lệnh defer sẽ là giá trị tại thời điểm gọi, chứ không phải lấy giá trị tại thời điêm thực thi
		defer fmt.Println(i)
	}
}

func printInt(a int) {
	fmt.Println("Value trong function được defer", a)
}

func worker(i int, wg *sync.WaitGroup) {
	defer wg.Done()

	fmt.Printf("Worker %d doing job\n", i)
	time.Sleep(2 * time.Second)
}
