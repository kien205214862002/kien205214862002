package main

import "fmt"
sáafsafsasadsasa
func main() {sadsafsafsafsa
	// Tạo slice
	// Nếu truyền giá trị vào cặp [] thì là tạo array
	// Nếu không truyền giá trị vào cặp [] thì là tạo slice
	a := []int{1, 3, 4, 7, 8}
	fmt.Println("a:", a)

	// Tạo slice từ một slice/array cho trước (từ vị trí start đến end - 1)
	// Cú pháp: a[start:end-1]
	b := a[1:4]
	fmt.Println("b:", b)
	c := a[1:]
	fmt.Println("c:", c)
	d := a[:3]
	fmt.Println("d:", d)

	// Khai báo 1 slice string có length và capacity là 3
	// e := make([]string, 3)
	// Khai báo 1 slice string có length là 5 và capacity là 5
	e := make([]string, 3, 5)
	e[0] = "Go"
	e[1] = "Javascript"
	e[2] = "Python"
	fmt.Println("e:", e)

	// Thay đổi giá trị của phần tử trong slice
	nums := [...]int{1, 5, 3, 7, 8, 2}

	s1 := nums[1:5]
	fmt.Println("array nums trước khi thay đổi:", nums)
	s1[1] = 10
	fmt.Println("array nums sau khi thay đổi slice s1:", nums)

	s2 := nums[3:]
	s2[0] = 15
	fmt.Println("array nums sau khi thay đổi slice s2:", nums)

	// length - capacity
	colors := []string{"red", "green", "blue", "black", "orange", "pink", "white", "yellow"}
	s3 := colors[1:4]
	fmt.Println("s3:", s3, "length:", len(s3), "capacity:", cap(s3))

	// Thêm phần tử vào slice, cú pháp: append(s T[], ...v)
	// slice mới luôn tăng length, nhưng capacity thì có thể có hoặc không
	s3 = append(s3, "grey", "brown")
	fmt.Println("s3 sau khi append:", s3, "length:", len(s3), "capacity:", cap(s3))

	// Thêm vào đầu slice - Lưu ý: sẽ gây ra việc cấp phát lại vùng nhớ và làm những phần tử đang tồn tại trong slice sẽ bị sao chép một lần nữa => hiệu suất sẽ thấp hơn là thêm vào cuối
	s3 = append([]string{"orange", "pink", "cyan"}, s3...)
	fmt.Println("s3 sau khi append lần 2:", s3, "length:", len(s3), "capacity:", cap(s3))

	// Thêm phần tử vào vị trí x trong slice: kết hợp append và copy
	x := 2                 // vị trí muốn thêm vào
	s3 = append(s3, "")    // thêm phần tử zero value vào cuối slice
	copy(s3[x+1:], s3[x:]) // lùi những phần tử từ vị trí x trở về sau
	s3[x] = "white"        // gán giá trị cho vị trí x
	fmt.Println("s3 sau khi append vào vị trí x = 2:", s3)

	// thêm nhiều phần tử vào trị trí y = 4 trong slice
	y := 4
	addedColors := []string{"A", "B", "C"}
	s3 = append(s3, addedColors...)
	copy(s3[y+len(addedColors):], s3[y:])
	copy(s3[y:], addedColors)
	fmt.Println("s3 sau khi append nhiều phần tử vào vị trí y = 4:", s3)

	// Xoá phần tử trong slice
	n := []int{1, 2, 3, 4, 5, 6}
	// Xoá cuối N phần tử: n = n[:len(n)-N]
	n = n[:len(n)-1]
	fmt.Println("slice n sau khi xoá cuối:", n)

	// Xoá đầu N phần tử: n = n[N:]
	n = n[1:]
	fmt.Println("slice n sau khi xoá đầu:", n)

	// Xoá từ vị trí x, N phần tử: n = append(n[:x], n[x+N:]...)
	n = append(n[:2], n[2+1:]...)
	fmt.Println("slice n sau khi xoá giữa:", n)

	// Đưa slice vào làm tham số hàm
	m := []int{1, 2, 3, 4}
	fmt.Println("slice m trước khi gọi hàm double:", m)
	double(m)
	fmt.Println("slice m sau khi gọi hàm double:", m)

}

func double(n []int) {
	for i := range n {
		n[i] *= 2
	}
}
