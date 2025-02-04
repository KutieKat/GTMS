USE GTMS;

--ChucVuHDBV
--INSERT INTO DanhSachChucVuHDBV VALUES  ('6/18/2019','6/18/2019',1,N'Chủ tịch hội đồng');
--INSERT INTO DanhSachChucVuHDBV VALUES ('6/18/2019','6/18/2019',1,N'Thư ký');
--INSERT INTO DanhSachChucVuHDBV VALUES ('6/18/2019','6/18/2019',1,N'Ủy viên');
--INSERT INTO DanhSachChucVuHDBV VALUES ('6/18/2019','6/18/2019',1,N'Phản biện');

--ChuDe
INSERT INTO DanhSachHuongNghienCuu VALUES  ('6/18/2019','6/18/2019',1,N'Điện toán đám mây');
INSERT INTO DanhSachHuongNghienCuu VALUES ('6/18/2019','6/18/2019',1,N'Big Data');
INSERT INTO DanhSachHuongNghienCuu VALUES ('6/18/2019','6/18/2019',1,N'Cơ sở dữ liệu mờ');
INSERT INTO DanhSachHuongNghienCuu VALUES ('6/18/2019','6/18/2019',1,N'Cơ sở dữ liệu suy diễn');

--KhoaDaoTao
SET IDENTITY_INSERT [dbo].[DanhSachKhoaDaoTao] ON 
INSERT [dbo].[DanhSachKhoaDaoTao] ([MaKhoaDaoTao], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [TenKhoaDaoTao], [TenVietTat], [ThoiGianBatDau], [ThoiGianKetThuc]) VALUES (1, CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Khóa 7 (2012)', N'K7', CAST(N'2012-09-05T00:00:00.0000000' AS DateTime2), CAST(N'2016-06-10T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[DanhSachKhoaDaoTao] ([MaKhoaDaoTao], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [TenKhoaDaoTao], [TenVietTat], [ThoiGianBatDau], [ThoiGianKetThuc]) VALUES (2, CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Khóa 8 (2013)', N'K8', CAST(N'2013-09-05T00:00:00.0000000' AS DateTime2), CAST(N'2017-06-10T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[DanhSachKhoaDaoTao] ([MaKhoaDaoTao], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [TenKhoaDaoTao], [TenVietTat], [ThoiGianBatDau], [ThoiGianKetThuc]) VALUES (3, CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Khóa 9 (2014)', N'K9', CAST(N'2014-09-05T00:00:00.0000000' AS DateTime2), CAST(N'2018-06-10T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[DanhSachKhoaDaoTao] ([MaKhoaDaoTao], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [TenKhoaDaoTao], [TenVietTat], [ThoiGianBatDau], [ThoiGianKetThuc]) VALUES (4, CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Khóa 7 (2015)', N'K10', CAST(N'2015-09-05T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-10T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[DanhSachKhoaDaoTao] ([MaKhoaDaoTao], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [TenKhoaDaoTao], [TenVietTat], [ThoiGianBatDau], [ThoiGianKetThuc]) VALUES (5, CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Khóa 7 (2016)', N'K11', CAST(N'2016-09-05T00:00:00.0000000' AS DateTime2), CAST(N'2020-06-10T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[DanhSachKhoaDaoTao] ([MaKhoaDaoTao], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [TenKhoaDaoTao], [TenVietTat], [ThoiGianBatDau], [ThoiGianKetThuc]) VALUES (6, CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Khóa 7 (2017)', N'K12', CAST(N'2017-09-05T00:00:00.0000000' AS DateTime2), CAST(N'2021-06-10T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[DanhSachKhoaDaoTao] ([MaKhoaDaoTao], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [TenKhoaDaoTao], [TenVietTat], [ThoiGianBatDau], [ThoiGianKetThuc]) VALUES (7, CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Khóa 7 (2018)', N'K13', CAST(N'2018-09-05T00:00:00.0000000' AS DateTime2), CAST(N'2022-06-10T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[DanhSachKhoaDaoTao] OFF

--HocKy
INSERT INTO DanhSachHocKy VALUES ('6/18/2019','6/18/2019',1,N'Học kỳ 1 2018-2019','2018-2019','9/1/2018','1/31/2019');
INSERT INTO DanhSachHocKy VALUES ('6/18/2019','6/18/2019',1,N'Học kỳ 2 2018-2019','2018-2019','1/2/2019','7/1/2019');
INSERT INTO DanhSachHocKy VALUES ('6/18/2019','6/18/2019',1,N'Học kỳ 1 2017-2018','2017-2018','9/1/2017','1/31/2018');
INSERT INTO DanhSachHocKy VALUES ('6/18/2019','6/18/2019',1,N'Học kỳ 2 2017-2018','2017-2018','1/2/2018','7/1/2019');
--HoiDongBaoVe
--INSERT INTO DanhSachHoiDongBaoVe VALUES  ('6/18/2019','6/18/2019',1,N'Hội đồng số 1','9/1/2018','1/31/2019',1);
--INSERT INTO DanhSachHoiDongBaoVe VALUES  ('6/18/2019','6/18/2019',1,N'Hội đồng số 2','9/1/2018','1/31/2019',1);

-- Khoa
INSERT INTO DanhSachKhoa VALUES ('6/18/2019', '6/18/2019', 1, N'Công nghệ phần mềm', 'CNPM');
INSERT INTO DanhSachKhoa VALUES ('6/18/2019', '6/18/2019', 1, N'Khoa học máy tính', 'KHMT');
INSERT INTO DanhSachKhoa VALUES ('6/18/2019', '6/18/2019', 1, N'Hệ thống thông tin', 'HTTT');
INSERT INTO DanhSachKhoa VALUES ('6/18/2019', '6/18/2019', 1, N'Kĩ thuật máy tính ', 'KTMT');
INSERT INTO DanhSachKhoa VALUES ('6/18/2019', '6/18/2019', 1, N'Mạng máy tính và truyền thông', 'MMT&TT');

--Lop
SET IDENTITY_INSERT [dbo].[DanhSachLop] ON 
INSERT [dbo].[DanhSachLop] ([MaLop], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [TenLop], [TenVietTat], [MaKhoa], [HeDaoTao], [MaKhoaDaoTao]) VALUES (1, CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Phần mềm chất lượng 2016.2', N'PMCL2016.2', 1, N'Chất lượng cao', 5)
INSERT [dbo].[DanhSachLop] ([MaLop], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [TenLop], [TenVietTat], [MaKhoa], [HeDaoTao], [MaKhoaDaoTao]) VALUES (2, CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Khoa học máy tính 2016', N'KHMT2016', 2, N'Đại trà', 5)
INSERT [dbo].[DanhSachLop] ([MaLop], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [TenLop], [TenVietTat], [MaKhoa], [HeDaoTao], [MaKhoaDaoTao]) VALUES (3, CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Hệ thống thông tin 2016', N'HTTT2016', 3, N'Đại trà', 5)
INSERT [dbo].[DanhSachLop] ([MaLop], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [TenLop], [TenVietTat], [MaKhoa], [HeDaoTao], [MaKhoaDaoTao]) VALUES (4, CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Kĩ thuật máy tính 2016', N'KTMT2016', 4, N'Đại trà', 5)
INSERT [dbo].[DanhSachLop] ([MaLop], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [TenLop], [TenVietTat], [MaKhoa], [HeDaoTao], [MaKhoaDaoTao]) VALUES (5, CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Mạng máy tính và truyền thông 2016', N'MMTT2016', 5, N'Đại trà', 5)
SET IDENTITY_INSERT [dbo].[DanhSachLop] OFF

--SinhVien
INSERT [dbo].[DanhSachSinhVien] ([MaSinhVien], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [HoVaTen], [MaLop], [GioiTinh], [NgaySinh], [Email], [QueQuan], [DiaChi], [SoDienThoai]) VALUES (N'SV190001', CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Lê Thị Ánh Nguyệt', 5, N'Nữ', CAST(N'1998-06-17T00:00:00.0000000' AS DateTime2), N'nguyetlemoon@gmail.com', N'Thành phố Pleiku, tỉnh Gia Lai', N'ktx khu A DHQG', N'0983936649')
INSERT [dbo].[DanhSachSinhVien] ([MaSinhVien], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [HoVaTen], [MaLop], [GioiTinh], [NgaySinh], [Email], [QueQuan], [DiaChi], [SoDienThoai]) VALUES (N'SV190002', CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Nguyễn Thị Anh', 3,  N'Nữ', CAST(N'1998-02-08T00:00:00.0000000' AS DateTime2), N'nguyenthianh@gmail.com', N'Huyện Hương Khê, tỉnh Hà Tĩnh', N'ktx khu A DHQG', N'0973936649')
INSERT [dbo].[DanhSachSinhVien] ([MaSinhVien], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [HoVaTen], [MaLop], [GioiTinh], [NgaySinh], [Email], [QueQuan], [DiaChi], [SoDienThoai]) VALUES (N'SV190003', CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Tạ Thị Hòa', 3,  N'Nữ', CAST(N'1998-02-21T00:00:00.0000000' AS DateTime2), N'toathihoa@gmail.com', N'huyện Bình Sơn, tỉnh Quảng Ngãi', N'ktx khu A DHQG', N'0974936649')
INSERT [dbo].[DanhSachSinhVien] ([MaSinhVien], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [HoVaTen], [MaLop], [GioiTinh], [NgaySinh], [Email], [QueQuan], [DiaChi], [SoDienThoai]) VALUES (N'SV190004', CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Ngô Minh Nhật', 4,  N'Nam', CAST(N'1998-02-08T00:00:00.0000000' AS DateTime2), N'ngominhnhat@gmail.com', N'Huyện Hương Khê, tỉnh Hà Tĩnh', N'ktx khu A DHQG', N'0833936649')
INSERT [dbo].[DanhSachSinhVien] ([MaSinhVien], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [HoVaTen], [MaLop], [GioiTinh], [NgaySinh], [Email], [QueQuan], [DiaChi], [SoDienThoai]) VALUES (N'SV190005', CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Nguyễn Hiếu Hiền', 2,  N'Nữ', CAST(N'1998-01-09T00:00:00.0000000' AS DateTime2), N'nguyenhieuhien@gmail.com', N'Huyện Hương Khê, tỉnh Hà Tĩnh', N'ktx khu A DHQG', N'0733936649')
INSERT [dbo].[DanhSachSinhVien] ([MaSinhVien], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [HoVaTen], [MaLop], [GioiTinh], [NgaySinh], [Email], [QueQuan], [DiaChi], [SoDienThoai]) VALUES (N'SV190006', CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Nguyễn Thảo Nguyên', 2,  N'Nữ', CAST(N'1998-01-06T00:00:00.0000000' AS DateTime2), N'nguyenthithaonguyen@gmail.com', N'huyện Cần Giờ, Thành phố Hồ Chí Minh', N'ktx khu A DHQG', N'09873936649')
INSERT [dbo].[DanhSachSinhVien] ([MaSinhVien], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [HoVaTen], [MaLop], [GioiTinh], [NgaySinh], [Email], [QueQuan], [DiaChi], [SoDienThoai]) VALUES (N'SV190007', CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Huỳnh Nhật Minh', 5,  N'Nam', CAST(N'1998-06-09T00:00:00.0000000' AS DateTime2), N'huynhnhatminh@gmail.com', N'quận 11, thành phố Hồ Chí Minh', N'ktx khu A DHQG', N'0923936649')


--GiangVien
INSERT [dbo].[DanhSachGiangVien] ([MaGiangVien], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [HoVaTen], [GioiTinh], [NgaySinh], [Email], [SoDienThoai], [QueQuan], [DiaChi], [DonViCongTac], [MaKhoa], [HocVi], [HocHam]) VALUES (N'GV190001', CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Phạm Thi Vương', N'Nam', CAST(N'1980-02-23T00:00:00.0000000' AS DateTime2), N'vuongpt@uit.edu.vn', N'0123456789', N'Thanh Hóa', N'Thành Phố Hồ Chí Minh',N'Trường Đại học Công Nghệ Thông Tin', 1, N'Tiến sĩ', N'Chưa có')
INSERT [dbo].[DanhSachGiangVien] ([MaGiangVien], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [HoVaTen], [GioiTinh], [NgaySinh], [Email], [SoDienThoai], [QueQuan], [DiaChi], [DonViCongTac],[MaKhoa], [HocVi], [HocHam]) VALUES (N'GV190002', CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Nguyễn Tấn Trần Minh Khang', N'Nam', CAST(N'1980-02-23T00:00:00.0000000' AS DateTime2), N'khangnttm@uit.edu.vn', N'0123456789', N'Thành Phố Hồ Chí Minh', N'Trường Đại học Công Nghệ Thông Tin',N'Thành Phố Hồ Chí Minh', 1, N'Tiến sĩ', N'Chưa có')
INSERT [dbo].[DanhSachGiangVien] ([MaGiangVien], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [HoVaTen], [GioiTinh], [NgaySinh], [Email], [SoDienThoai], [QueQuan], [DiaChi], [DonViCongTac],[MaKhoa], [HocVi], [HocHam]) VALUES (N'GV190003', CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Phan Nguyệt Minh', N'Nữ', CAST(N'1980-02-23T00:00:00.0000000' AS DateTime2), N'minhpn@uit.edu.vn', N'0123456789', N'Thành Phố Hồ Chí Minh', N'Thành Phố Hồ Chí Minh',N'Trường Đại học Công Nghệ Thông Tin', 1, N'Thạc sĩ', N'Chưa có')

INSERT [dbo].[DanhSachGiangVien] ([MaGiangVien], [ThoiGianTao], [ThoiGianCapNhat], [TrangThai], [HoVaTen], [GioiTinh], [NgaySinh], [Email], [SoDienThoai], [QueQuan], [DiaChi], [DonViCongTac],[MaKhoa], [HocVi], [HocHam]) VALUES (N'GV190004', CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2019-06-18T00:00:00.0000000' AS DateTime2), 1, N'Phan Nguyệt Minh', N'Nữ', CAST(N'1980-02-23T00:00:00.0000000' AS DateTime2), N'minhpn@uit.edu.vn', N'0123456789', N'Thành Phố Hồ Chí Minh', N'Thành Phố Hồ Chí Minh',N'Trường Đại học Công Nghệ Thông Tin', null, N'Thạc sĩ', N'Chưa có')

--DoAn
--INSERT INTO DanhSachDoAn VALUES ('6/18/2019','6/18/2019',1,N'Hệ thống quản lý đồ án tốt nghiệp',1,1,'https://drive.google.com/drive/u/0/folders/15pAARno2cdgajrRz_pFp3NJMy3JGkpMT','5/19/1890');
--INSERT INTO DanhSachDoAn VALUES ('6/18/2019','6/18/2019',1,N'Hệ thống quản lý đồ án chuỗi của hàng tiện lợi',2,2,'https://drive.google.com/drive/u/0/folders/15pAARno2cdgajrRz_pFp3NJMy3JGkpMT','5/19/1890');