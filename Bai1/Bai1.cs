using System;

namespace Bai1
{
    internal class Bai1
    {
      
        public static void Main1(string[] args)
        {
            // In hướng dẫn người dùng nhập số lượng phần tử n
            Console.Write("insert n (1 <= n <= 100000): ");

            // Kiểm tra đầu vào: phải ép sang int và nằm trong đoạn [1, 100000]
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 1 || n > 100000)
            {
                // In thông báo giá trị nhập không hợp lệ
                Console.WriteLine("invalid!");

                // Kết thúc chương trình sớm để tránh lỗi cấp phát bộ nhớ
                return;
            }

            // Khởi tạo mảng số nguyên 'a' có kích thước đúng n ô nhớ
            int[] a = new int[n];

            // Duyệt từ chỉ số 0 đến n - 1 để nhận dữ liệu cho từng ô nhớ
            for (int i = 0; i < n; i++)
            {
                // In thông điệp nhập giá trị tương ứng tại vị trí chỉ số i
                Console.Write($"insert a[{i}]: ");

                // Chuyển chuỗi nhập thành số nguyên int và gán trực tiếp vào phần tử a[i]
                a[i] = int.Parse(Console.ReadLine()!);
            }

            // Dùng kiểu long để lưu tổng, tránh bị tràn số int khi cộng dồn tối đa 100,000 phần tử int
            long sum = 0;

            // Khởi tạo max từ phần tử đầu tiên a[0] để làm mốc so sánh
            int max = a[0];

            // Khởi tạo min từ phần tử đầu tiên a[0] để làm mốc so sánh
            int min = a[0];

            // Biến đếm số lượng phần tử âm
            int negaCount = 0;

            // Biến đếm số lượng phần tử dương
            int posCount = 0;

            // Biến đếm số lượng phần tử bằng 0
            int zeroCount = 0;

            // Duyệt mảng đúng 1 lượt duy nhất từ 0 đến n - 1 để tính toàn bộ số liệu thống kê 
            for (int j = 0; j < n; j++)
            {
                // Cộng dồn giá trị phần tử a[j] vào biến tổng sum kiểu long
                sum += a[j];

                // Kiểm tra và đếm số lượng phần tử âm, dương hoặc bằng 0
                if (a[j] < 0) negaCount++;
                else if (a[j] > 0) posCount++;
                else zeroCount++;

                // Cập nhật max nếu phát hiện phần tử a[j] lớn hơn mốc max hiện tại
                if (a[j] > max) max = a[j];

                // Cập nhật min nếu phát hiện phần tử a[j] nhỏ hơn mốc min hiện tại
                if (a[j] < min) min = a[j];
            }

            // Ép kiểu sum sang double trước khi chia để thực hiện phép chia số thực, giữ lại phần thập phân
            double sumAVG = (double)sum / n;

            // In kết quả đếm số lượng phần tử âm, dương và bằng 0
            Console.WriteLine($"\nNegative: {negaCount}; Positive: {posCount}; Zero(es): {zeroCount}");

            // In kết quả Max, Min, Sum và SumAVG (định dạng F2 để lấy tối đa 2 chữ số thập phân)
            Console.WriteLine($"Max: {max}; Min: {min}; Sum: {sum}; SumAVG: {sumAVG:F2}");

            // Khởi tạo con trỏ left trỏ vào vị trí đầu tiên của mảng (chỉ số 0)
            int left = 0;

            // Khởi tạo con trỏ right trỏ vào vị trí cuối cùng của mảng (chỉ số n - 1)
            int right = n - 1;

            // Tiến hành đảo ngược mảng tại chỗ (In-place) bằng cách dịch 2 con trỏ vào giữa cho đến khi gặp nhau
            while (left < right)
            {
                // Lưu tạm giá trị a[left] vào biến trung gian temp để không bị ghi đè dữ liệu khi hoán đổi
                int temp = a[left];

                // Gán giá trị ở vị trí right sang vị trí left
                a[left] = a[right];

                // Gán giá trị tạm thời vào vị trí right
                a[right] = temp;

                // Dịch con trỏ left sang phải 1 đơn vị
                left++;

                // Dịch con trỏ right sang trái 1 đơn vị
                right--;
            }

            // In ra mảng sau khi đã đảo ngược hoàn tất, các phần tử phân cách bằng khoảng trắng
            Console.WriteLine("array after switched: " + string.Join(" ", a));
        }
    }
}