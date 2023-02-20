package main

import (
	"fmt"
	"time"
)

func queryFormServerA(ch chan<- string) {
	time.Sleep(time.Second * 4)
	ch <- "Data from server A"
}

func queryFormServerB(ch chan<- string) {
	time.Sleep(time.Second * 6)
	ch <- "Data from server B"
}

func main() {
	chA := make(chan string)
	chB := make(chan string)

	// query
	go queryFormServerA(chA)
	go queryFormServerB(chB)

	for {
		time.Sleep(time.Second)
		select {
		case dataA := <-chA:
			fmt.Println(dataA)
			return
		case dataB := <-chB:
			fmt.Println(dataB)
			return
		default:
			fmt.Println("Default case")
		}
	}

}
