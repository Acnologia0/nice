// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App4;
/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();     
    }

    public ObservableCollection<HuongDoiTuong.KhachHang> khachhangs { get; set; } = new();
    public ObservableCollection<HuongDoiTuong.SanPham> sanphams { get; set; } = new();
    public ObservableCollection<HuongDoiTuong.DonDatHang> dondathangs { get; set; } = new();
    public ObservableCollection<HuongDoiTuong.DinhDuong> dinhduongs { get; set; } = new();
    public ObservableCollection<HuongDoiTuong.ThanhPhan> thanhphans { get; set; } = new();
    public ObservableCollection<HuongDoiTuong.Loai> loais { get; set; } = new();
    public void AddKhachHang(object sender, RoutedEventArgs e)
    {
        var khachhang = new HuongDoiTuong.KhachHang();
        khachhang.ID = khachhangs.Count;
        khachhangs.Add(khachhang);
    }
    public void AddSanPham(object sender, RoutedEventArgs e)
    {
        var sanpham = new HuongDoiTuong.SanPham();
        sanpham.ID = sanphams.Count;
        sanphams.Add(sanpham);
    }
    public void AddDonDatHang(object sender, RoutedEventArgs e)
    {
        var donhang = new HuongDoiTuong.DonDatHang();
        donhang.ID = dondathangs.Count;
        dondathangs.Add(donhang);
    }

    private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
    {
        // C# code to create a new window
        var newWindow = new MainWindow();
        newWindow.Activate();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }
}
