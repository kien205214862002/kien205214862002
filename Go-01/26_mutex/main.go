package main

import (
	"fmt"
	"sync"
)

var x = 0

func increment(wg *sync.WaitGroup, m *sync.Mutex) {
	m.Lock()
	x = x + 1
	m.Unlock()
	wg.Done()
}

func main() {
	wg := sync.WaitGroup{}
	m := sync.Mutex{}

	for i := 1; i <= 1000; i++ {
		wg.Add(1)
		go increment(&wg, &m)
	}

	wg.Wait()

	fmt.Println("Value of x:", x)
}
