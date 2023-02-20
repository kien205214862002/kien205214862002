package main

import "fmt"

// trong Go có một interface như sau
// type Stringer interface {
// 	String() string
// }

type Person struct {
	Name string
	Age  int
}

func (p Person) String() string {
	return fmt.Sprintf("Name: %s, Age: %d", p.Name, p.Age)
}

func main() {
	p1 := Person{"Alice", 18}
	fmt.Println(p1)

	p2 := Person{"Bob", 25}
	fmt.Println(p2)

}
