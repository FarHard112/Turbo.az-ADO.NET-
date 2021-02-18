using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turbo.az
{
    class ClssCommonMethods
    {
        ClssInfoAdapter clssInfoAdapter = new ClssInfoAdapter();

        public void SetLookUpEditTypeId(LookUpEdit lookUpEdit, string Typed_id)
        {
            lookUpEdit.Properties.DataSource = clssInfoAdapter.GetGeneralInfoForID(Typed_id);
            lookUpEdit.Properties.DisplayMember = "NAME";
            lookUpEdit.Properties.ValueMember = "ID";
        }
   public     void SetBrand(LookUpEdit lookUpEdit)
        {

            lookUpEdit.Properties.DataSource = clssInfoAdapter.GetBrands();
            lookUpEdit.Properties.DisplayMember = "NAME";
            lookUpEdit.Properties.ValueMember = "ID";

        }

        public void SetModel(LookUpEdit lookUpEdit,LookUpEdit lookUpEditMarka)
        {
            lookUpEdit.Properties.DataSource = clssInfoAdapter.GetModels(lookUpEditMarka.EditValue.ToString());
            lookUpEdit.Properties.DisplayMember = "NAME";
            lookUpEdit.Properties.ValueMember = "ID";
        }
    }
}
