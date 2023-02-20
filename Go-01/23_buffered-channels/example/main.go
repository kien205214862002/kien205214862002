package main

import (
	"fmt"
	"time"
)

// Thực hiện m công việc với n goroutines

// 1. Dùng buffered channel để làm queue (chứa các công việc)
// 2. Chạy n goroutines thực hiện công việc (worker)

const (
	numOfJobs    = 100
	numOfWorkers = 5
)

func run(name string, jobs <-chan int) {
	// Truy cập vào danh sách công việc lấy ra và thực hiện nó
	for job := range jobs {
		fmt.Printf("Worker %s is doing job %d\n", name, job)
		time.Sleep(time.Second * 2)
	}

	fmt.Printf("Worker %s done", name)
}

// Tạo danh sách công việc
func start() chan int {
	jobs := make(chan int, 10)

	// Duyệt qua danh sách công việc và đưa vào channel jobs
	go func() {
		for i := 1; i <= numOfJobs; i++ {
			jobs <- i
			fmt.Printf("Job %d has been enqueued\n", i)
		}
		// Chạy hết vòng for, đồng nghĩa đã đưa tất cả công việc vào hàng đợi => đóng channel jobs
		close(jobs)
	}()

	return jobs
}

func main() {
	jobs := start()

	for i := 1; i <= numOfWorkers; i++ {
		name := fmt.Sprintf("%d", i)
		go run(name, jobs)
	}

	time.Sleep((time.Second * 2 * numOfJobs) / numOfWorkers)
}
