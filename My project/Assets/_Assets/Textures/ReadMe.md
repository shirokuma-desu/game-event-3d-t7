phải đặt điều kiện bài toán.
ngày giao hàng vs ngày tạo hóa đơn là một.

What : Xuất hàng
Who : super admin và các admin có quyền truy cập
Where : tại kho hàng.
When : khi bán hàng
Why :  ???


system test
unit test
Api test (intergration test)


Làm về tính năng nhập hàng vào kho 
quản lý sản phẩm trong kho ( CRUD ).

sau khi tọa xog hóa đơn sản phảm
người dùng sẽ được truy cập đến một trang 
gọi là sắp xếp sản phẩm 
trong đó chưa thông tin sản phẩm
lô hàng
vị trí chỗ cần để 
mã qr

quản lý sản phẩm

nhập kho có 3 bước
bước 1 chọn sản phẩm muốn nhập vào kho ( thường là chọn hết sp) nhưng nếu kho ko còn chỗ trống cho người dùng chọn sản phẩm kèm

tạo một danh sách hàng trả lại.

bước 2 : hệ thống sẽ tự động sắp xếp vị trí của các sản phẩm trong kho theo thứ tự 
sản phẩm có date dài hạn sẽ xếp xuống dưới, sản phẩm có date thấp sẽ xếp lên cao 
mỗi cột sẽ chứa 1 sản phẩm cụ thể.
hoặc người dùng có thể tự thay đổi theo ý bản thân muốn trong một số trường hợp đặc biệt.

Bước 3 : Sau khi confifrm vị trí, website sẽ gen ra mã qr có chứa thông tin cần thiết và thông tin hiện thêm
gồm tên sp, mã lô hàng. người dùng có thể tải ảnh về để đ em đi in (sẽ có tính năng kết hợp với máy in để in ) 

(issue của bước 1)
Trong TH : Nhập Hàng
trong th1 : người dùng tạo hóa đơn xong sẽ nhập hết hàng trong hóa đơn luôn 
trong th2 : người dùng tạo xong hóa đơn nhưng sẽ không nhập được hết số lượng hàng cụ thể ( nên quan tâm số thùng, 1 thùng = 1 kệ hàng ) 
vậy thì người dùng có thể nhập hàng theo quy trình nào ? 
QT1 : chọn các hóa đợn nhập rồi sau đó cho chọn số lượng sp trong đơn hàng muốn nhập. 
TH1: Hóa đơn nhập  còn 3 thùng, kệ trống còn 3 thì OK (perfect world)
TH2: Hóa đơn nhập  còn 4 thùng, kệ trống còn 2 thùng thì sao ?? 
Solution cho người dùng nhập số lượng thùng hàng trong mỗi hóa đơn.
Vậy thì hiển thị như thế nào cho hợp lý.
mã lô hàng,tên sản phẩm ,tên nhà cung cấp, HSD,NSX, đVT, giá nhập, SL Thùng, SL thực nhập , SL kệ trống còn lại, ngày nhập hàng, ngày tạo hóa đơn 

cần filter + hiển thị hợp lý 


(issue của bước 2)
cần thêm một bước để tìm sản phẩm cụ thể muốn sắp xếp.


log thêm phần authentication và tuần tiếp theo
update log phần tạo hóa đơn nhập hàng

tài liệu phân tích cho phần quản lý account
tài liệu phân tích cho phần quản lý hóa đơn nhập
tài liệu cho phần phân tích nhập hàng

log 25/10/2023


- VẼ FLOW SƠ BỘ CHO TỪNG CHỨC NĂNG LỚN 
- ước lượng sơ bộ các chức năng cho flow.
- 56


quản lý nhân viên data a để test phải là data thật, data test 
quản lý là nhìn được việc cái mình quản lý biến động ntn tốt hay xấu để người quản lý điều chỉnh.
suppport và hỗ trợ là làm hộ.


