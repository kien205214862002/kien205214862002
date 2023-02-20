package main

import (
	"fmt"
	"sync"
	"time"
)

func worker(i int, wg *sync.WaitGroup) {
	fmt.Printf("Worker %d doing job\n", i)
	time.Sleep(2 * time.Second)

	// Xử lý xong trong worker, gọi wg.Done()
	wg.Done()
}

func main() {
	wg := sync.WaitGroup{}

	for i := 1; i <= 5; i++ {
		wg.Add(1)
		go worker(i, &wg)
	}

	// Làm sao để block main goroutine cho đến khi tất cả các worker goroutine hoàn tất
	wg.Wait()

	fmt.Println("All goroutines done")
}
