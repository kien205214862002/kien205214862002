package main

import (
	"fmt"
)

// Khai báo interface này có nghĩa là: bất kì kiểu dữ liệu nào, miễn là nó có method Speak() string thì đều được gọi là implement interface Animal
type Animal interface {
	Speak() string
}

type Dog struct {
	nickname string
	age      int
}

func (d Dog) Speak() string {
	return "Gâu gâu"
}

type Duck struct{}

func (d Duck) Speak() string {
	return "Cạp cạp"
}

// ============= Multiple interface =============

type SalaryCalculator interface {
	displaySalary()
}

type LeaveCalculator interface {
	calculateLeavesLeft() int
}

type EmployeeOperations interface {
	SalaryCalculator
	LeaveCalculator
}

type Employee struct {
	fullName    string
	salary      int
	bonus       int
	totalLeaves int // tổng số ngày phép
	leavesTaken int // số ngày đã nghĩ
}

func (e Employee) displaySalary() {
	fmt.Printf("Employee %s has salary %d\n", e.fullName, e.salary+e.bonus)
}

func (e Employee) calculateLeavesLeft() int {
	return e.totalLeaves - e.leavesTaken
}

// ============= Multiple interface =============

func main() {
	animals := []Animal{Dog{"lulu", 2}, Duck{}}
	for _, animal := range animals {
		fmt.Println(animal.Speak())
	}

	describe(100)
	describe("Hello Gopher")
	str := struct {
		name string
	}{
		name: "Alice",
	}
	describe(str)

	// Ứng dụng empty interface
	product := make(map[string]interface{})
	product["name"] = "Iphone 14 Promax"
	product["price"] = 32000000

	var i interface{} = "Gopher"
	fmt.Printf("type: %T, value: %v\n", i, i)

	s := i.(string)
	fmt.Printf("type: %T, value: %v\n", s, s)

	f, ok := i.(float64)
	fmt.Printf("type: %T, value: %v, ok: %t\n", f, f, ok)

	var emp EmployeeOperations = Employee{"Dan Nguyen", 1000, 300, 12, 5}
	emp.displaySalary()
	fmt.Println("Leaves left:", emp.calculateLeavesLeft())
}

// Empty interface: interface{}
// any là alias của interface{}
func describe(i any) {
	fmt.Printf("Type: %T, Value: %v\n", i, i)
}
