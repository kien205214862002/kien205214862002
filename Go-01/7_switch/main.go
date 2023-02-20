package main

import "fmt"

func main() {
	month := 13

	switch month {
	case 1:
		fmt.Println("Tháng một")
	case 2:
		fmt.Println("Tháng hai")
	case 3:
		fmt.Println("Tháng ba")
	case 4:
		fmt.Println("Tháng bốn")
	case 5:
		fmt.Println("Tháng năm")
	case 6:
		fmt.Println("Tháng sáu")
	case 7:
		fmt.Println("Tháng bảy")
	case 8:
		fmt.Println("Tháng tám")
	case 9:
		fmt.Println("Tháng chín")
	case 10:
		fmt.Println("Tháng mười")
	case 11:
		fmt.Println("Tháng mười một")
	case 12:
		fmt.Println("Tháng mười hai")
	default:
		fmt.Println("Tháng không hợp lệ")
	}

	letter := "a"
	switch letter {
	case "a", "e", "i", "o", "u":
		fmt.Println("Nguyên âm")
	default:
		fmt.Println("Không phải nguyên âm")
	}

	n := 8.0
	switch {
	case n >= 8:
		fmt.Println("Giỏi")
	case n >= 6.5:
		fmt.Println("Giỏi")
	case n >= 5:
		fmt.Println("Trung Bình")
	default:
		fmt.Println("Yếu")
	}

	// switch fallthrough
	m := 80
	switch {
	case m < 100:
		fmt.Printf("%d nhỏ hơn 100\n", m)
		fallthrough // tiếp tục kiểm tra các case tiếp theo chứ không thoát khỏi switch
	case m < 200:
		fmt.Printf("%d nhỏ hơn 200\n", m)
		fallthrough
	case m < 300:
		fmt.Printf("%d nhỏ hơn 300\n", m)
	}

}
