using System;

namespace Bai4_MangDongVaCapacity
{
    // Lớp DynamicArray<T> tự định nghĩa mô phỏng cơ chế mảng động (như List<T>) bằng mảng tĩnh T[]
    public class DynamicArray<T>
    {
        // Mảng tĩnh nội bộ dùng để lưu trữ dữ liệu thực tế
        private T[] items;

        // Thuộc tính Count lưu số lượng phần tử hợp lệ hiện có trong mảng động
        public int Count { get; private set; }

        // Thuộc tính Capacity trả về tổng sức chứa (kích thước mảng tĩnh nội bộ)
        public int Capacity => items.Length;

        // Biến toàn cục dùng để theo dõi tổng số lượt phần tử phải sao chép khi mở rộng mảng phục vụ kiểm thử
        public static int TotalCopiedCount = 0;

        // Constructor khởi tạo mảng động với dung lượng mặc định là 4
        public DynamicArray(int capacity = 4)
        {
            // Kiểm tra dung lượng đầu vào: từ chối nếu dung lượng là số âm
            if (capacity < 0)
            {
                // Ném ngoại lệ ArgumentOutOfRangeException khi capacity < 0 theo đúng yêu cầu đề bài
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity cannot be negative!");
            }

            // Cấp phát mảng tĩnh nội bộ với kích thước được chỉ định
            items = new T[capacity];

            // Đặt số phần tử hợp lệ ban đầu bằng 0
            Count = 0;
        }

        // Phương thức nội bộ tự động mở rộng dung lượng mảng gấp đôi khi mảng đầy
        private void EnsureCapacity()
        {
            // Tính dung lượng mới: gấp đôi Capacity hiện tại (hoặc bằng 1 nếu Capacity ban đầu = 0)
            int newCapacity = Capacity == 0 ? 1 : Capacity * 2;

            // In thông điệp ghi nhận việc mở rộng mảng và số lượng phần tử cần sao chép
            Console.WriteLine($"  [CAPACITY EXPANDED] {Capacity} -> {newCapacity}. Copied elements: {Count}");

            // Cộng dồn số lượt phần tử phải sao chép vào biến thống kê
            TotalCopiedCount += Count;

            // Cấp phát mảng mới có dung lượng gấp đôi mảng cũ
            T[] newItems = new T[newCapacity];

            // Vòng lặp sao chép toàn bộ Count phần tử từ mảng cũ sang mảng mới
            for (int i = 0; i < Count; i++)
            {
                // Chép giá trị từng phần tử sang mảng mới
                newItems[i] = items[i];
            }

            // Chuyển con trỏ items sang quản lý mảng mới đã mở rộng
            items = newItems;
        }

        // Phương thức thêm phần tử mới vào cuối mảng động
        public void Add(T item)
        {
            // Kiểm tra nếu số phần tử thực tế đã đạt tới ngưỡng dung lượng tối đa hiện tại
            if (Count == Capacity)
            {
                // Tiến hành mở rộng mảng trước khi chèn phần tử mới
                EnsureCapacity();
            }

            // Đặt phần tử mới vào ô trống tại chỉ số Count
            items[Count] = item;

            // Tăng biến đếm số phần tử hợp lệ Count lên 1 đơn vị
            Count++;
        }

        // Bộ truy xuất Indexer cho phép đọc/ghi phần tử qua cú pháp array[index]
        public T this[int index]
        {
            get
            {
                // Kiểm tra chỉ số nằm ngoài phạm vi cho phép [0, Count - 1]
                if (index < 0 || index >= Count)
                {
                    // Ném ngoại lệ ArgumentOutOfRangeException khi chỉ số không hợp lệ
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range!");
                }

                // Trả về giá trị phần tử tại vị trí index
                return items[index];
            }
            set
            {
                // Kiểm tra chỉ số nằm ngoài phạm vi cho phép [0, Count - 1]
                if (index < 0 || index >= Count)
                {
                    // Ném ngoại lệ ArgumentOutOfRangeException khi chỉ số không hợp lệ
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range!");
                }

                // Gán giá trị mới cho phần tử tại vị trí index
                items[index] = value;
            }
        }
    }

    internal class Program
    {
        // Điểm khởi chạy chính của chương trình C#
        public static void Main(string[] args)
        {
            // In tiêu đề chương trình kiểm thử DynamicArray
            Console.WriteLine("=== DYNAMIC ARRAY TEST PROGRAM ===");

            // Khởi tạo mảng động với Capacity mặc định bằng 4
            DynamicArray<int> arr = new DynamicArray<int>();

            // 1. Thêm lần lượt các số từ 1 đến 10 và in Count, Capacity sau mỗi lần thêm
            Console.WriteLine("\n--- 1. ADD ELEMENTS 1 TO 10 ---");
            for (int i = 1; i <= 10; i++)
            {
                // In thông báo thêm phần tử i
                Console.WriteLine($"Adding element {i}:");

                // Gọi phương thức Add để thêm phần tử i vào mảng
                arr.Add(i);

                // In ra giá trị Count và Capacity hiện tại sau khi thêm
                Console.WriteLine($"  -> Count = {arr.Count}, Capacity = {arr.Capacity}");
            }

            // 2. Đọc và cập nhật phần tử qua Indexer
            Console.WriteLine("\n--- 2. READ AND UPDATE INDEXER ---");

            // In giá trị phần tử tại vị trí index = 0
            Console.WriteLine($"Element at index 0: {arr[0]}");

            // In giá trị phần tử tại vị trí index = 4
            Console.WriteLine($"Element at index 4: {arr[4]}");

            // Cập nhật giá trị phần tử đầu tiên qua indexer
            Console.WriteLine("Updating arr[0] = 999...");
            arr[0] = 999;

            // In giá trị mới vừa cập nhật tại index = 0
            Console.WriteLine($"New value at index 0: {arr[0]}");

            // 3. Kiểm thử các ngoại lệ của Indexer (Index âm và Index = Count)
            Console.WriteLine("\n--- 3. TEST INDEXER EXCEPTIONS ---");

            // Thử truy cập indexer với chỉ số âm (-1)
            try
            {
                Console.Write("Testing negative index (-1): ");
                int val = arr[-1];
            }
            catch (Exception ex)
            {
                // Bắt ngoại lệ ArgumentOutOfRangeException do index < 0
                Console.WriteLine($"\n  -> [Success] {ex.GetType().Name}: {ex.Message}");
            }

            // Thử truy cập indexer với chỉ số bằng Count (index = arr.Count)
            try
            {
                Console.Write($"Testing index equal to Count ({arr.Count}): ");
                int val = arr[arr.Count];
            }
            catch (Exception ex)
            {
                // Bắt ngoại lệ ArgumentOutOfRangeException do index >= Count
                Console.WriteLine($"\n  -> [Success] {ex.GetType().Name}: {ex.Message}");
            }

            // 4. Kiểm tra dữ liệu cũ giữ nguyên thứ tự sau khi mở rộng
            Console.WriteLine("\n--- 4. CHECK DATA ORDER AFTER EXPANSION ---");
            Console.Write("Current array elements: ");

            // Duyệt từ 0 đến Count - 1 để in toàn bộ mảng
            for (int i = 0; i < arr.Count; i++)
            {
                // In từng giá trị phân cách bằng khoảng trắng
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();

            // 5. Thống kê tổng số lượt sao chép phần tử phục vụ phân tích chi phí khấu hao
            Console.WriteLine($"\nTotal copied elements during all expansions: {DynamicArray<int>.TotalCopiedCount}");
        }
    }
}