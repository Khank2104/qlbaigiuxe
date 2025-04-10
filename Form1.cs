using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qlbaigiuxe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAddParkingLot_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string capacityText = txtCapacity.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(capacityText))
            {
                lblMessage.Text = "Vui lòng nhập đầy đủ Tên, Địa chỉ và Sức chứa!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            if (!int.TryParse(capacityText, out int capacity) || capacity <= 0)
            {
                lblMessage.Text = "Sức chứa phải là số nguyên dương!";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            using (var context = new ParkingLotContext())
            {
                // Kiểm tra tên có bị trùng không (bỏ qua hoa thường)
                bool isNameDuplicate = context.ParkingLots
                                        .Any(p => p.Name.ToLower() == name.ToLower());

                // Kiểm tra địa chỉ có bị trùng không
                bool isAddressDuplicate = context.ParkingLots
                                           .Any(p => p.Address.ToLower() == address.ToLower());

                // Hiển thị thông báo phù hợp
                if (isNameDuplicate && isAddressDuplicate)
                {
                    lblMessage.Text = "Tên và Địa chỉ đã tồn tại!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                else if (isNameDuplicate)
                {
                    lblMessage.Text = "Tên bãi đỗ đã tồn tại!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                else if (isAddressDuplicate)
                {
                    lblMessage.Text = "Địa chỉ bãi đỗ đã tồn tại!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                // Nếu không bị trùng thì thêm mới
                var parkingLot = new ParkingLot
                {
                    Name = name,
                    Address = address,
                    Capacity = capacity
                };

                context.ParkingLots.Add(parkingLot);
                context.SaveChanges();

                lblMessage.Text = "Thêm bãi đỗ thành công!";
                lblMessage.ForeColor = Color.Green;

                txtName.Clear();
                txtAddress.Clear();
                txtCapacity.Clear();

                var timer = new System.Windows.Forms.Timer();
                timer.Interval = 3000;
                timer.Tick += (s, args) =>
                {
                    lblMessage.Text = "";
                    timer.Stop();
                };
                timer.Start();
            }
        }


    }
}
