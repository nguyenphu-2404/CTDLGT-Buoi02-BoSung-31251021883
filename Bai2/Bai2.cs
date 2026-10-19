using System;

namespace Bai2
{
    internal class Program
    {
        public static void Main2(string[] args)
        {
            // Cấp phát mảng 'data' gồm 6 ô nhớ
            int[] data = new int[6];

            // Khởi tạo dữ liệu ban đầu cho 5 ô đầu tiên của mảng
            data[0] = 4; data[1] = 7; data[2] = 2; data[3] = 9; data[4] = 5;

            // Khai báo biến count lưu số lượng phần tử thực tế đang có dữ liệu hợp lệ
            int count = 5;

            // In tiêu đề mảng ban đầu
            Console.WriteLine("Normal array:");

            // Gọi phương thức PrintArray để in mảng
            PrintArray(data, count);

            // 1. Gọi phương thức Insert để chèn giá trị 99 vào vị trí chỉ số index = 2
            Insert(data, ref count, 2, 99);

            // In tiêu đề thao tác chèn
            Console.WriteLine("\nAfter inserting 99 at index 2:");

            // Hiển thị mảng đã được cập nhật (lúc này count = 6)
            PrintArray(data, count);

            // 2. Gọi phương thức RemoveAt để xóa phần tử tại vị trí chỉ số index = 1
            RemoveAt(data, ref count, 1);

            // In tiêu đề thao tác xóa
            Console.WriteLine("\nAfter removing element at index 1:");

            // Hiển thị mảng đã được cập nhật (lúc này count = 5)
            PrintArray(data, count);

            // --- BỔ SUNG ĐẦY ĐỦ TẤT CẢ CÁC CA KIỂM THỬ ---

            // 3. Chèn tại đầu mảng (index = 0) khi mảng chưa đầy
            Insert(data, ref count, 0, 100);
            Console.WriteLine("\nAfter inserting 100 at the head (index = 0):");
            PrintArray(data, count); // count = 6 (Mảng đầy)

            // 4. Thử chèn khi mảng đã đầy để kiểm thử InvalidOperationException
            try
            {
                Console.WriteLine("\nTrying to insert into a FULL array:");
                Insert(data, ref count, 0, 88);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught exception: {ex.GetType().Name} - {ex.Message}");
            }

            // 5. Xóa tại đầu mảng (index = 0)
            RemoveAt(data, ref count, 0);
            Console.WriteLine("\nAfter removing element at head (index = 0):");
            PrintArray(data, count); // count = 5

            // 6. Chèn tại cuối mảng (index = count)
            Insert(data, ref count, count, 200);
            Console.WriteLine("\nAfter inserting 200 at the end (index = count):");
            PrintArray(data, count); // count = 6

            // 7. Xóa phần tử cuối mảng (index = count - 1)
            RemoveAt(data, ref count, count - 1);
            Console.WriteLine("\nAfter removing the last element (index = count - 1):");
            PrintArray(data, count); // count = 5

            // 8. Thử index âm khi chèn
            try
            {
                Console.WriteLine("\nTrying to insert at invalid negative index (-1):");
                Insert(data, ref count, -1, 50);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught exception: {ex.GetType().Name} - {ex.Message}");
            }

            // 9. Thử index BẰNG COUNT khi xóa
            try
            {
                Console.WriteLine($"\nTrying to remove at index equal to count ({count}):");
                RemoveAt(data, ref count, count);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught exception: {ex.GetType().Name} - {ex.Message}");
            }
        }

        // Phương thức Insert chèn giá trị value vào vị trí index trong mảng data
        public static void Insert(int[] data, ref int count, int index, int value)
        {
            // Kiểm tra chỉ số index không hợp lệ: nhỏ hơn 0 hoặc lớn hơn số phần tử hiện có (count)
            if (index < 0 || index > count)
            {
                // Ném ngoại lệ ArgumentOutOfRangeException khi chỉ số vượt ngoài phạm vi [0, count]
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid insert index!");
            }

            // Kiểm tra dung lượng mảng: số phần tử hợp lệ count không được vượt quá sức chứa data.Length
            if (count >= data.Length)
            {
                // Ném ngoại lệ InvalidOperationException khi mảng đã hết ô nhớ trống
                throw new InvalidOperationException("Array is full, cannot insert!");
            }

            // Dịch chuyển các phần tử từ vị trí count lùi về index + 1 sang phải 1 ô (duyệt từ phải sang trái)
            for (int i = count; i > index; i--)
            {
                // ĐÃ SỬA LỖI: Chép giá trị từ ô i - 1 sang ô i phía bên phải để mở ô trống tại vị trí index
                data[i] = data[i - 1];
            }

            // Đặt giá trị mới value vào vị trí index đã được giải phóng
            data[index] = value;

            // Tăng số lượng phần tử hợp lệ count lên 1 đơn vị sau khi chèn xong
            count++;
        }

        // Phương thức RemoveAt xóa phần tử tại vị trí index khỏi mảng data
        public static void RemoveAt(int[] data, ref int count, int index)
        {
            // Kiểm tra chỉ số index không hợp lệ: nhỏ hơn 0 hoặc lớn hơn hoặc bằng count
            if (index < 0 || index >= count)
            {
                // Ném ngoại lệ ArgumentOutOfRangeException khi chỉ số không tồn tại trong khoảng [0, count - 1]
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid remove index!");
            }

            // Dịch chuyển các phần tử từ vị trí index + 1 đến count - 1 sang trái 1 ô (duyệt từ trái sang phải)
            for (int i = index; i < count - 1; i++)
            {
                // Chép giá trị từ ô i + 1 bên phải đè về ô i bên trái để lấp vị trí bị xóa
                data[i] = data[i + 1];
            }

            // Giảm số lượng phần tử hợp lệ count đi 1 đơn vị sau khi xóa xong
            count--;
        }

        // Phương thức hỗ trợ hiển thị các phần tử hợp lệ trong mảng theo chuẩn định dạng
        static void PrintArray(int[] data, int count)
        {
            // In mở ngoặc mảng
            Console.Write("[");

            // Duyệt từ chỉ số 0 đến count - 1 để hiển thị từng giá trị thực tế
            for (int i = 0; i < count; i++)
            {
                // In từng giá trị phần tử, thêm dấu phẩy ngăn cách nếu chưa phải phần tử cuối
                Console.Write(data[i] + (i == count - 1 ? "" : ", "));
            }

            // Đóng ngoặc mảng, in số lượng count và gọi WriteLine() để xuống dòng sạch sẽ
            Console.WriteLine($"] (count = {count})");
        }
    }
}