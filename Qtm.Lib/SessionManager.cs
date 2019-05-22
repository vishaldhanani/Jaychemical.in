using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Security.Cryptography;
using System.Data;
using System.Configuration;

namespace Qtm.Lib
{
    public class SessionManager
    {
        #region Varible
        public static string CartTableName = "Cart";
        #endregion

        #region Create User Session
        public static void CreateUserSession(HttpContext hc)
        {
            hc.Session.Add("AgentCode", "");
            hc.Session.Add("AgentName", "");
            hc.Session.Add("Comapny", "");
            hc.Session.Add("CustomerId", "");
            hc.Session.Add("CustPriceGrp", "");
            hc.Session.Add("ConsigneeId", "");
            hc.Session.Add("ProductGroup", "");
            hc.Session.Add("ItemCategoryCode", "");
        }

        #endregion

        #region Add To User Session
        public static void AddToUserSession(HttpContext hc, string AgenCode, string AgentName)
        {
            hc.Session["AgentCode"] = AgenCode;
            hc.Session.Timeout = 60;
            hc.Session["AgentName"] = AgentName;
            hc.Session.Timeout = 60;

            //hc.Session["FullName"] = FullName;
            //hc.Session.Timeout = 60;
        }
        public static void AddLoginTimeSession(HttpContext hc,  DateTime LogInTime)
        {
            hc.Session["LoginTimeSession"] = LogInTime;
            hc.Session.Timeout = 60;
            
        }

        public static void AddCompanyToSession(HttpContext hc, string Company)
        {
            hc.Session["Comapny"] = Company;
            hc.Session.Timeout = 60;
        }
        public static void AddCustomerNoToSession(HttpContext hc, string CustNo)
        {
            hc.Session["CustNo"] = CustNo;
            hc.Session.Timeout = 60;
        }
        public static void AddCustomerNameToSession(HttpContext hc, string CustNo)
        {
            hc.Session["CustName"] = CustNo;
            hc.Session.Timeout = 60;
        }
        public static void AddStartDateToSession(HttpContext hc, DateTime StartDate)
        {
            hc.Session["StartDate"] = StartDate;
            hc.Session.Timeout = 60;
        }

        public static void AddCustomerToSession(HttpContext hc, string CustomerId)
        {
            hc.Session["CustomerId"] = CustomerId;
            hc.Session.Timeout = 60;
        }

        public static void AddCustomerPriceGroupToSession(HttpContext hc, string CustPriceGrp)
        {
            hc.Session["CustPriceGrp"] = CustPriceGrp;
            hc.Session.Timeout = 60;
        }

        public static void AddSplCustomerPriceGroupToSession(HttpContext hc, string CustPriceGrp)
        {
            hc.Session["SplCustPriceGrp"] = CustPriceGrp;
            hc.Session.Timeout = 60;
        }
        public static void AddProductGroupCodeDefault(HttpContext hc, string PriceGroup)
        {
            hc.Session["ProductGroup"] = PriceGroup;
            hc.Session.Timeout = 60;
        }

        public static void AddConsigneeToSession(HttpContext hc, string ConsigneeId)
        {
            hc.Session["ConsigneeId"] = ConsigneeId;
            hc.Session.Timeout = 60;
        }

        public static void AddItemCategoryToSession(HttpContext hc, string ItemCategoryCode)
        {
            hc.Session["ItemCategoryCode"] = ItemCategoryCode;
            hc.Session.Timeout = 60;
        }

        public static void AddUserTypeSession(HttpContext hc, string UserType)
        {
            hc.Session["UserType"] = UserType;
            hc.Session.Timeout = 60;
        }

        #endregion
        #region End Cust discount percentage
        public static void AddCustDiscountPercentage(HttpContext hc, string CustDisPercentage)
        {
            hc.Session["CustDisPercentage"] = CustDisPercentage;
            hc.Session.Timeout = 60;
        }
        #endregion
        // CustDisPercentage

        #region End User Session
        public static void EndUserSession(HttpContext hc)
        {
            hc.Session["AgentCode"] = null;
            hc.Session["AgentName"] = null;
            //hc.Session["AdminUserType"] = null;
        }

        #endregion

        #region End Front User Session
        public static void EndFrontUserSession(HttpContext hc)
        {
            hc.Session["UserName"] = null;
            hc.Session["UserId"] = null;
            //hc.Session["UserType"] = null;
        }

        #endregion

        #region Check User Session
        public static void CheckUserSession(HttpContext hc, string loginurl)
        {
            //if (hc.Session["AdminUserName"].Equals(String.Empty))
            if (hc.Session["AgentCode"] == null || hc.Session["AgentCode"] == string.Empty || hc.Session["UserName"] == null || hc.Session["UserName"] == string.Empty)
            {
                hc.Response.Redirect(loginurl);
            }
            //if (hc.Session["AgentCode"] == null || hc.Session["CustName"] == null || hc.Session["userName"] == null || hc.Session["CompanyCode"] == null || hc.Session["CustomerId"] == null || hc.Session["CustPriGrp"] == null || hc.Session["Customer"] == null || hc.Session["Consignee"] == null)
        }
        #endregion

        //#region Check Customer Session
        //public static void CheckCustomerSession(HttpContext hc, string url)
        //{
        //    //if (hc.Session["AdminUserName"].Equals(String.Empty))
        //    if (hc.Session["CustomerNo"] == null || hc.Session["CustomerNo"] == string.Empty)
        //    {
        //        hc.Response.Redirect(url);
        //    }
        //    //if (hc.Session["AgentCode"] == null || hc.Session["CustName"] == null || hc.Session["userName"] == null || hc.Session["CompanyCode"] == null || hc.Session["CustomerId"] == null || hc.Session["CustPriGrp"] == null || hc.Session["Customer"] == null || hc.Session["Consignee"] == null)
        //}
        //#endregion


        //#region  Item Category Session
        //public static void CheckItemCategory(HttpContext hc, string loginurl)
        //{
        //    if (hc.Session["ItemCategoryCode"] == null)
        //    {
        //        hc.Response.Redirect(loginurl, false);
        //    }
        //}
        //#endregion

        #region Check User Session Front
        public static void CheckUserSession_Front(HttpContext hc, string loginurl)
        {
            if (hc.Session["UserName"] == null)
            {
                hc.Response.Redirect(loginurl, false);
            }
        }
        #endregion

        #region Get User Name
        public static string GetEmailAddress(HttpContext hc)
        {
            if (hc.Session["UserName"] != null)
            {
                return hc.Session["UserName"].ToString();
            }
            else
            {
                return String.Empty;
            }
        }
        #endregion
        #region
        public static string GetLoginTime(HttpContext hc)
        {
            if (hc.Session["LoginTimeSession"] != null)
            {
                return hc.Session["LoginTimeSession"].ToString();
            }
            else
            {
                return String.Empty;
            }
        }
        #endregion
        #region Get ItemCategoryCode
        public static string GetItemCategoryCode(HttpContext hc)
        {
            if (hc.Session["ItemCategoryCode"] != null)
            {
                return hc.Session["ItemCategoryCode"].ToString();
            }
            else
            {
                return String.Empty;
            }
        }
        #endregion

        #region Get UserType
        public static string GetUserType(HttpContext hc)
        {
            if (hc.Session["UserType"] != null)
            {
                return hc.Session["UserType"].ToString();
            }
            else
            {
                return String.Empty;
            }
        }
        #endregion



        #region Get AgentCode
        public static string GetAgentCode(HttpContext hc)
        {
            if (hc.Session["AgentCode"] == null)
            {
                return String.Empty;
            }
            else
            {
                return hc.Session["AgentCode"].ToString();
            }
        }
        #endregion

        #region Get CompanyCode
        public static string GetCompanyCode(HttpContext hc)
        {
            if (hc.Session["Comapny"] == null)
            {
                return String.Empty;
            }
            else
            {
                return hc.Session["Comapny"].ToString();
            }
        }

        #endregion
        #region "Get Customer No."
        public static string GetCustomerNoCode(HttpContext hc)
        {
            if (hc.Session["CustNo"] == null)
            {
                return String.Empty;
            }
            else
            {
                return hc.Session["CustNo"].ToString();
            }
        }
        public static string GetCustomerNameCode(HttpContext hc)
        {
            if (hc.Session["CustName"] == null)
            {
                return String.Empty;
            }
            else
            {
                return hc.Session["CustName"].ToString();
            }
        }

        #endregion
        #region "Get Customer No."
        public static DateTime GetStartDate(HttpContext hc)
        {
            if (hc.Session["StartDate"] == null)
            {
                return Convert.ToDateTime(System.DBNull.Value);
            }
            else
            {
                return Convert.ToDateTime(hc.Session["StartDate"]);
            }
        }
        #endregion


        #region GetCustomerId
        public static string GetCustomerId(HttpContext hc)
        {
            if (hc.Session["CustomerId"] == null)
            {
                return String.Empty;
            }
            else
            {
                return hc.Session["CustomerId"].ToString();
            }
        }
        #endregion

        #region GetCustomerPriceGroup
        public static string GetCustomerPriceGroup(HttpContext hc)
        {
            if (hc.Session["CustPriceGrp"] == null)
            {
                return String.Empty;
            }
            else
            {
                return hc.Session["CustPriceGrp"].ToString();
            }
        }
        #endregion
        #region GetSplCustomerPriceGroup
        public static string GetSplCustomerPriceGroup(HttpContext hc)
        {
            if (hc.Session["SplCustPriceGrp"] == null)
            {
                return String.Empty;
            }
            else
            {
                return hc.Session["SplCustPriceGrp"].ToString();
            }
        }
        #endregion


        public static string GetProductGroupCode(HttpContext hc)
        {
            if (hc.Session["ProductGroup"] == null)
            {
                return String.Empty;
            }
            else
            {
                return hc.Session["ProductGroup"].ToString();
            }
        }

        #region Get CustDisPercentage
        public static string GetCustDisPercentage(HttpContext hc)
        {
            if (hc.Session["CustDisPercentage"] == null)
            {
                return String.Empty;
            }
            else
            {
                return hc.Session["CustDisPercentage"].ToString();
            }
        }
        #endregion

        #region Get User Type
        public static string GetAgentName(HttpContext hc)
        {
            if (hc.Session["AgentName"].ToString() == "")
            {
                return String.Empty;
            }
            else
            {
                return hc.Session["AgentName"].ToString();
            }
        }
        #endregion

        #region Get Consignee Id
        public static string GetConsigneeId(HttpContext hc)
        {
            if (hc.Session["ConsigneeId"].ToString() == "")
            {
                return String.Empty;
            }
            else
            {
                return hc.Session["ConsigneeId"].ToString();
            }
        }
        #endregion

        #region Cart
        public static void CartTable(HttpContext hc)
        {
            DataTable dtTemp = new DataTable();
            DataSet ds = new DataSet();
            DataColumn[] dc = new DataColumn[1];
            dtTemp.TableName = CartTableName;
            dtTemp.Columns.Add("noField", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("ProductCode", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("ProductName", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("Quantity", System.Type.GetType("System.Decimal"));
            dtTemp.Columns.Add("Rate", System.Type.GetType("System.Decimal"));
            dtTemp.Columns.Add("SellingPrice", System.Type.GetType("System.Decimal"));
            dtTemp.Columns.Add("UOM", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("VariantCode", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("Remark", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("CSellingPrice", System.Type.GetType("System.Decimal"));
            dtTemp.Columns.Add("DiscPercentage", System.Type.GetType("System.Decimal"));
            dtTemp.Columns.Add("DiscPrice", System.Type.GetType("System.Decimal"));
            dtTemp.Columns.Add("ExtField_Cprice", System.Type.GetType("System.Decimal"));
            dtTemp.Columns.Add("ExtField_SellingPrice", System.Type.GetType("System.Decimal"));

            // Added below two fields by vishal for Dyes
            dtTemp.Columns.Add("DyesDiscPrice", System.Type.GetType("System.Decimal"));
            dtTemp.Columns.Add("DyesNetAmount", System.Type.GetType("System.Decimal"));
            dtTemp.Columns.Add("BillPrice", System.Type.GetType("System.Decimal"));




            ds.Tables.Add(dtTemp);
            hc.Session[CartTableName] = ds;
        }

        public static string AddProductToCart(string ProductCode, string ProductName, string Variant, string UOM, decimal Price, decimal Qty, decimal discountpercentage)
        {
            try
            {
                bool IsProductExists = false;
                DataSet dsCart = (DataSet)HttpContext.Current.Session[CartTableName];
                if (dsCart.Tables[CartTableName] != null && dsCart.Tables[CartTableName].Rows.Count >= Convert.ToInt32(ConfigurationManager.AppSettings["MaxOrderItem"]))
                {
                    // added by raj shah - MaxOrderItem is defined in web.config but not initialed in 
                    return "You can not add product more than" + Convert.ToInt32(ConfigurationManager.AppSettings["MaxOrderItem"]) + ".";
                }
                else
                {
                    if (dsCart.Tables[CartTableName] != null)
                    {
                        if (dsCart.Tables[CartTableName].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsCart.Tables[CartTableName].Rows.Count; i++)
                            {
                                if (ProductCode.ToLower().Equals(dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString().ToLower()))
                                {
                                    if (Variant.ToLower().Equals(dsCart.Tables[CartTableName].Rows[i]["VariantCode"].ToString().ToLower()))
                                    {
                                        IsProductExists = true;
                                        // Because Case for Same Item is not consider while development and now change it for same item add. ( 06-10-2015 ) 
                                        //break;                                        
                                    }
                                }                                
                            }
                            if (IsProductExists)
                            {
                                
                                //int inum = dsCart.Tables[CartTabsleName].Rows.Count;
                                //return "Product is already exists.";
                                return "Product is already added in the Cart.";
                                //inum = inum + 1;
                                //DataRow dr = dsCart.Tables[CartTableName].NewRow();
                                //dr["noField"] = Convert.ToString(inum);
                                //dr["ProductCode"] = ProductCode;
                                //dr["ProductName"] = ProductName;
                                //dr["Quantity"] = Qty;
                                //dr["Rate"] = Price;
                                //dr["SellingPrice"] = Qty * Price;
                                //dr["UOM"] = UOM;
                                //dr["VariantCode"] = Variant;
                                //dr["Remark"] = "";
                                //dr["CSellingPrice"] = 0;
                                //dr["DiscPrice"] = 0;
                                //dr["DiscPercentage"] = discountpercentage;
                                //dr["ExtField_Cprice"] = 0;
                                //dr["ExtField_SellingPrice"] = 0;

                                //// Added below two fields by vishal for Dyes
                                //dr["DyesDiscPrice"] = 0;
                                //dr["DyesNetAmount"] = 0;
                                //dr["BillPrice"] = 0;

                                //dsCart.Tables[CartTableName].Rows.Add(dr);

                            }
                            else
                            {
                                int inum = dsCart.Tables[CartTableName].Rows.Count;
                                //return "Product is already exists.";
                                inum = inum + 1;

                                DataRow dr = dsCart.Tables[CartTableName].NewRow();
                                dr["noField"] = Convert.ToString(inum);
                                dr["ProductCode"] = ProductCode;
                                dr["ProductName"] = ProductName;
                                dr["Quantity"] = Qty;
                                dr["Rate"] = Price;
                                dr["SellingPrice"] = Qty * Price;
                                dr["UOM"] = UOM;
                                dr["VariantCode"] = Variant;
                                dr["Remark"] = "";
                                dr["CSellingPrice"] = 0;
                                dr["DiscPrice"] = 0;
                                dr["DiscPercentage"] = discountpercentage;
                                dr["ExtField_Cprice"] = 0;
                                dr["ExtField_SellingPrice"] = 0;

                                // Added below two fields by vishal for Dyes
                                dr["DyesDiscPrice"] = 0;
                                dr["DyesNetAmount"] = 0;
                                dr["BillPrice"] = 0;

                                dsCart.Tables[CartTableName].Rows.Add(dr);
                            }
                        }
                        else
                        {
                            int inum = dsCart.Tables[CartTableName].Rows.Count;
                            //return "Product is already exists.";
                            inum = inum + 1;
                            DataRow dr = dsCart.Tables[CartTableName].NewRow();
                            dr["noField"] = Convert.ToString(inum);
                            dr["ProductCode"] = ProductCode;
                            dr["ProductName"] = ProductName;
                            dr["Quantity"] = Qty;
                            dr["Rate"] = Price;
                            dr["SellingPrice"] = Qty * Price;
                            dr["UOM"] = UOM;
                            dr["VariantCode"] = Variant;
                            dr["Remark"] = "";
                            dr["CSellingPrice"] = 0;
                            dr["DiscPrice"] = 0;
                            dr["DiscPercentage"] = discountpercentage;
                            dr["ExtField_Cprice"] = 0;
                            dr["ExtField_SellingPrice"] = 0;

                            // Added below two fields by vishal for Dyes
                            dr["DyesDiscPrice"] = 0;
                            dr["DyesNetAmount"] = 0;
                            dr["BillPrice"] = 0;

                            dsCart.Tables[CartTableName].Rows.Add(dr);
                        }
                    }
                    else
                    {
                        return "Sorry cart is not available.";
                    }
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateProductToCart(string ProductCode, string ProductName, string Variant, string UOM, decimal Price, decimal Qty, string Remark, decimal CPrice, decimal DisPercentage)
        {
            try
            {
                DataSet dsCart = (DataSet)HttpContext.Current.Session[CartTableName];


                if (dsCart.Tables[CartTableName] != null)
                {
                    if (dsCart.Tables[CartTableName].Rows.Count > 0)
                    {

                        for (int i = 0; i < dsCart.Tables[CartTableName].Rows.Count; i++)
                        {
                            // Added by Raj Shah 06/10/2015
                            // Changed the code for the purpose of Veifying which item to update.


                            if (ProductCode.ToLower().Equals(dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString().ToLower()))
                            {
                                if ((dsCart.Tables[CartTableName].Rows[i]["VariantCode"].ToString().ToLower()) == "")
                                {


                                    Decimal DisPrice = (CPrice * DisPercentage) / 100;
                                    Decimal Amt = ((CPrice * Qty) * DisPercentage) / 100;
                                    Decimal AmountAfterDiscount = (CPrice * Qty) - Amt;
                                    Decimal CpriceDiscount = (CPrice - DisPrice);

                                    // Added below two fields by vishal for Dyes
                                    Decimal DyesDisAmt = (Price * DisPercentage) / 100;
                                    Decimal DyesNetPrice = (Price - DyesDisAmt);

                                    int inum = dsCart.Tables[CartTableName].Rows.Count;
                                    //return "Product is already exists.";

                                    dsCart.Tables[CartTableName].Rows[i]["noField"] = Convert.ToString(inum);
                                    dsCart.Tables[CartTableName].Rows[i]["ProductCode"] = ProductCode;
                                    dsCart.Tables[CartTableName].Rows[i]["ProductName"] = ProductName;
                                    dsCart.Tables[CartTableName].Rows[i]["Quantity"] = Qty;
                                    dsCart.Tables[CartTableName].Rows[i]["Rate"] = Price;
                                    dsCart.Tables[CartTableName].Rows[i]["SellingPrice"] = Math.Round(AmountAfterDiscount); //Qty * CPrice;
                                    dsCart.Tables[CartTableName].Rows[i]["UOM"] = UOM;
                                    dsCart.Tables[CartTableName].Rows[i]["VariantCode"] = Variant;
                                    dsCart.Tables[CartTableName].Rows[i]["Remark"] = Remark;
                                    // Removed Roudingdue to the problem of Sales Order Line creation. - Raj Shah 24/09/2015
                                    dsCart.Tables[CartTableName].Rows[i]["CSellingPrice"] = (CPrice);
                                    dsCart.Tables[CartTableName].Rows[i]["DiscPrice"] = (DisPrice);
                                    dsCart.Tables[CartTableName].Rows[i]["DiscPercentage"] = DisPercentage;
                                    dsCart.Tables[CartTableName].Rows[i]["ExtField_Cprice"] = CpriceDiscount;
                                    dsCart.Tables[CartTableName].Rows[i]["ExtField_SellingPrice"] = Math.Round(Qty * CPrice);

                                    // Added below two fields by vishal for Dyes
                                    dsCart.Tables[CartTableName].Rows[i]["DyesDiscPrice"] = DyesDisAmt;
                                    dsCart.Tables[CartTableName].Rows[i]["DyesNetAmount"] = DyesNetPrice;

                                }
                                else
                                {
                                    if (ProductCode.ToLower().Equals(dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString().ToLower()))
                                    {
                                        // reverse for that 
                                        if ((dsCart.Tables[CartTableName].Rows[i]["VariantCode"].ToString().ToLower()) != "")
                                        {
                                            int tempi = i + 1;
                                            if ((tempi).Equals(dsCart.Tables[CartTableName].Rows[i]["noField"].ToString().ToLower()))
                                            {
                                                Decimal DisPrice = (CPrice * DisPercentage) / 100;
                                                Decimal Amt = ((CPrice * Qty) * DisPercentage) / 100;
                                                Decimal AmountAfterDiscount = (CPrice * Qty) - Amt;
                                                Decimal CpriceDiscount = (CPrice - DisPrice);

                                                // Added below two fields by vishal for Dyes
                                                Decimal DyesDisAmt = (Price * DisPercentage) / 100;
                                                Decimal DyesNetPrice = (Price - DyesDisAmt);


                                                int inum = dsCart.Tables[CartTableName].Rows.Count;
                                                //return "Product is already exists.";

                                                dsCart.Tables[CartTableName].Rows[i]["noField"] = Convert.ToString(inum);
                                                dsCart.Tables[CartTableName].Rows[i]["ProductCode"] = ProductCode;
                                                dsCart.Tables[CartTableName].Rows[i]["ProductName"] = ProductName;
                                                dsCart.Tables[CartTableName].Rows[i]["Quantity"] = Qty;
                                                dsCart.Tables[CartTableName].Rows[i]["Rate"] = Price;
                                                dsCart.Tables[CartTableName].Rows[i]["SellingPrice"] = Math.Round(AmountAfterDiscount); //Qty * CPrice;
                                                dsCart.Tables[CartTableName].Rows[i]["UOM"] = UOM;
                                                dsCart.Tables[CartTableName].Rows[i]["VariantCode"] = Variant;
                                                dsCart.Tables[CartTableName].Rows[i]["Remark"] = Remark;
                                                // Removed Roudingdue to the problem of Sales Order Line creation. - Raj Shah 24/09/2015
                                                dsCart.Tables[CartTableName].Rows[i]["CSellingPrice"] = (CPrice);
                                                dsCart.Tables[CartTableName].Rows[i]["DiscPrice"] = (DisPrice);
                                                dsCart.Tables[CartTableName].Rows[i]["DiscPercentage"] = DisPercentage;
                                                dsCart.Tables[CartTableName].Rows[i]["ExtField_Cprice"] = CpriceDiscount;
                                                dsCart.Tables[CartTableName].Rows[i]["ExtField_SellingPrice"] = Math.Round(Qty * CPrice);

                                                // Added below two fields by vishal for Dyes
                                                dsCart.Tables[CartTableName].Rows[i]["DyesDiscPrice"] = DyesDisAmt;
                                                dsCart.Tables[CartTableName].Rows[i]["DyesNetAmount"] = DyesNetPrice;

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateProductToCartByNo(int no, string ProductCode, string ProductName, string Variant, string UOM, decimal Price, decimal Qty, string Remark, decimal CPrice, decimal DisPercentage)
        {
            try
            {
                DataSet dsCart = (DataSet)HttpContext.Current.Session[CartTableName];


                if (dsCart.Tables[CartTableName] != null)
                {
                    if (dsCart.Tables[CartTableName].Rows.Count > 0)
                    {

                        for (int i = 0; i < dsCart.Tables[CartTableName].Rows.Count; i++)
                        {
                            // Added by Raj Shah 06/10/2015
                            // Changed the code for the purpose of Veifying which item to update.


                            if (ProductCode.ToLower().Equals(dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString().ToLower()))
                            {
                                if ((dsCart.Tables[CartTableName].Rows[i]["VariantCode"].ToString().ToLower()) == "")
                                {


                                    Decimal DisPrice = (CPrice * DisPercentage) / 100;
                                    Decimal Amt = ((CPrice * Qty) * DisPercentage) / 100;
                                    Decimal AmountAfterDiscount = (CPrice * Qty) - Amt;
                                    Decimal CpriceDiscount = (CPrice - DisPrice);

                                    // Added below two fields by vishal for Dyes
                                    Decimal DyesDisAmt = (Price * DisPercentage) / 100;
                                    Decimal DyesNetPrice = (Price - DyesDisAmt);


                                    int inum = dsCart.Tables[CartTableName].Rows.Count;
                                    //return "Product is already exists.";

                                    dsCart.Tables[CartTableName].Rows[i]["noField"] = Convert.ToString(inum);
                                    dsCart.Tables[CartTableName].Rows[i]["ProductCode"] = ProductCode;
                                    dsCart.Tables[CartTableName].Rows[i]["ProductName"] = ProductName;
                                    dsCart.Tables[CartTableName].Rows[i]["Quantity"] = Qty;
                                    dsCart.Tables[CartTableName].Rows[i]["Rate"] = Price;
                                    dsCart.Tables[CartTableName].Rows[i]["SellingPrice"] = Math.Round(AmountAfterDiscount); //Qty * CPrice;
                                    dsCart.Tables[CartTableName].Rows[i]["UOM"] = UOM;
                                    dsCart.Tables[CartTableName].Rows[i]["VariantCode"] = Variant;
                                    dsCart.Tables[CartTableName].Rows[i]["Remark"] = Remark;
                                    // Removed Roudingdue to the problem of Sales Order Line creation. - Raj Shah 24/09/2015
                                    dsCart.Tables[CartTableName].Rows[i]["CSellingPrice"] = (CPrice);
                                    dsCart.Tables[CartTableName].Rows[i]["DiscPrice"] = (DisPrice);
                                    dsCart.Tables[CartTableName].Rows[i]["DiscPercentage"] = DisPercentage;
                                    dsCart.Tables[CartTableName].Rows[i]["ExtField_Cprice"] = CpriceDiscount;
                                    dsCart.Tables[CartTableName].Rows[i]["ExtField_SellingPrice"] = Math.Round(Qty * CPrice);

                                    // Added below two fields by vishal for Dyes
                                    dsCart.Tables[CartTableName].Rows[i]["DyesDiscPrice"] = DyesDisAmt;
                                    dsCart.Tables[CartTableName].Rows[i]["DyesNetAmount"] = DyesNetPrice;


                                }
                                else
                                {
                                    if (ProductCode.ToLower().Equals(dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString().ToLower()))
                                    {
                                        // reverse for that 
                                        if ((dsCart.Tables[CartTableName].Rows[i]["VariantCode"].ToString().ToLower()) != "")
                                        {

                                            if (dsCart.Tables[CartTableName].Rows[i]["noField"].ToString().ToLower() == Convert.ToString(no))
                                            {
                                                Decimal DisPrice = (CPrice * DisPercentage) / 100;
                                                Decimal Amt = ((CPrice * Qty) * DisPercentage) / 100;
                                                Decimal AmountAfterDiscount = (CPrice * Qty) - Amt;
                                                Decimal CpriceDiscount = (CPrice - DisPrice);

                                                // Added below two fields by vishal for Dyes
                                                Decimal DyesDisAmt = (Price * DisPercentage) / 100;
                                                Decimal DyesNetPrice = (Price - DyesDisAmt);


                                                int inum = dsCart.Tables[CartTableName].Rows.Count;
                                                //return "Product is already exists.";

                                                dsCart.Tables[CartTableName].Rows[i]["noField"] = Convert.ToString(no);
                                                dsCart.Tables[CartTableName].Rows[i]["ProductCode"] = ProductCode;
                                                dsCart.Tables[CartTableName].Rows[i]["ProductName"] = ProductName;
                                                dsCart.Tables[CartTableName].Rows[i]["Quantity"] = Qty;
                                                dsCart.Tables[CartTableName].Rows[i]["Rate"] = Price;
                                                dsCart.Tables[CartTableName].Rows[i]["SellingPrice"] = Math.Round(AmountAfterDiscount); //Qty * CPrice;
                                                dsCart.Tables[CartTableName].Rows[i]["UOM"] = UOM;
                                                dsCart.Tables[CartTableName].Rows[i]["VariantCode"] = Variant;
                                                dsCart.Tables[CartTableName].Rows[i]["Remark"] = Remark;
                                                // Removed Roudingdue to the problem of Sales Order Line creation. - Raj Shah 24/09/2015
                                                dsCart.Tables[CartTableName].Rows[i]["CSellingPrice"] = (CPrice);
                                                dsCart.Tables[CartTableName].Rows[i]["DiscPrice"] = (DisPrice);
                                                dsCart.Tables[CartTableName].Rows[i]["DiscPercentage"] = DisPercentage;
                                                dsCart.Tables[CartTableName].Rows[i]["ExtField_Cprice"] = CpriceDiscount;
                                                dsCart.Tables[CartTableName].Rows[i]["ExtField_SellingPrice"] = Math.Round(Qty * CPrice);

                                                // Added below two fields by vishal for Dyes
                                                dsCart.Tables[CartTableName].Rows[i]["DyesDiscPrice"] = DyesDisAmt;
                                                dsCart.Tables[CartTableName].Rows[i]["DyesNetAmount"] = DyesNetPrice;

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateProductToCartForDyes(string ProductCode, string ProductName, string Variant, string UOM, decimal Price, decimal Qty, string Remark, decimal AgentPrice, decimal DisPercentage, decimal BillPrice)
        {
            try
            {
                DataSet dsCart = (DataSet)HttpContext.Current.Session[CartTableName];
                if (dsCart.Tables[CartTableName] != null)
                {
                    if (dsCart.Tables[CartTableName].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsCart.Tables[CartTableName].Rows.Count; i++)
                        {
                            // Added by Raj Shah 06/10/2015
                            // Changed the code for the purpose of Veifying which item to update.


                            if (ProductCode.ToLower().Equals(dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString().ToLower()))
                            {
                                if ((dsCart.Tables[CartTableName].Rows[i]["VariantCode"].ToString().ToLower()) == "")
                                {
                                    Decimal DisPrice = (AgentPrice * DisPercentage) / 100;
                                    Decimal Amt = ((AgentPrice * Qty) * DisPercentage) / 100;
                                    Decimal AmountAfterDiscount = (AgentPrice * Qty) - Amt;
                                    Decimal CpriceDiscount = (AgentPrice - DisPrice);

                                    // Added below two fields by vishal for Dyes
                                    Decimal DyesDisAmt = (BillPrice * DisPercentage) / 100;
                                    Decimal DyesNetPrice = (BillPrice - DyesDisAmt);

                                    int inum = dsCart.Tables[CartTableName].Rows.Count;
                                    //return "Product is already exists.";

                                    dsCart.Tables[CartTableName].Rows[i]["noField"] = Convert.ToString(inum);
                                    dsCart.Tables[CartTableName].Rows[i]["ProductCode"] = ProductCode;
                                    dsCart.Tables[CartTableName].Rows[i]["ProductName"] = ProductName;
                                    dsCart.Tables[CartTableName].Rows[i]["Quantity"] = Qty;
                                    dsCart.Tables[CartTableName].Rows[i]["Rate"] = Price;
                                    dsCart.Tables[CartTableName].Rows[i]["SellingPrice"] = Math.Round(DyesNetPrice * Qty);//Math.Round(AmountAfterDiscount); //Qty * CPrice;
                                    dsCart.Tables[CartTableName].Rows[i]["UOM"] = UOM;
                                    dsCart.Tables[CartTableName].Rows[i]["VariantCode"] = Variant;
                                    dsCart.Tables[CartTableName].Rows[i]["Remark"] = Remark;
                                    // Removed Roudingdue to the problem of Sales Order Line creation. - Raj Shah 24/09/2015
                                    dsCart.Tables[CartTableName].Rows[i]["CSellingPrice"] = (BillPrice);
                                    dsCart.Tables[CartTableName].Rows[i]["DiscPrice"] = (DisPrice);
                                    dsCart.Tables[CartTableName].Rows[i]["DiscPercentage"] = DisPercentage;
                                    dsCart.Tables[CartTableName].Rows[i]["ExtField_Cprice"] = CpriceDiscount;
                                    dsCart.Tables[CartTableName].Rows[i]["ExtField_SellingPrice"] = Math.Round(Qty * BillPrice);

                                    // Added below three fields by vishal for Dyes
                                    dsCart.Tables[CartTableName].Rows[i]["DyesDiscPrice"] = DyesDisAmt;
                                    dsCart.Tables[CartTableName].Rows[i]["DyesNetAmount"] = DyesNetPrice;
                                    dsCart.Tables[CartTableName].Rows[i]["BillPrice"] = BillPrice;



                                }
                                else
                                {
                                    if (ProductCode.ToLower().Equals(dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString().ToLower()))
                                    {
                                        // reverse for that 
                                        if ((dsCart.Tables[CartTableName].Rows[i]["VariantCode"].ToString().ToLower()) != "")
                                        {
                                            int tempi = i + 1;
                                            if ((tempi).Equals(dsCart.Tables[CartTableName].Rows[i]["noField"].ToString().ToLower()))
                                            {
                                                Decimal DisPrice = (AgentPrice * DisPercentage) / 100;
                                                Decimal Amt = ((AgentPrice * Qty) * DisPercentage) / 100;
                                                Decimal AmountAfterDiscount = (AgentPrice * Qty) - Amt;
                                                Decimal CpriceDiscount = (AgentPrice - DisPrice);

                                                // Added below two fields by vishal for Dyes
                                                Decimal DyesDisAmt = (BillPrice * DisPercentage) / 100;
                                                Decimal DyesNetPrice = (BillPrice - DyesDisAmt);


                                                int inum = dsCart.Tables[CartTableName].Rows.Count;
                                                //return "Product is already exists.";

                                                dsCart.Tables[CartTableName].Rows[i]["noField"] = Convert.ToString(inum);
                                                dsCart.Tables[CartTableName].Rows[i]["ProductCode"] = ProductCode;
                                                dsCart.Tables[CartTableName].Rows[i]["ProductName"] = ProductName;
                                                dsCart.Tables[CartTableName].Rows[i]["Quantity"] = Qty;
                                                dsCart.Tables[CartTableName].Rows[i]["Rate"] = Price;
                                                dsCart.Tables[CartTableName].Rows[i]["SellingPrice"] = Math.Round(DyesNetPrice * Qty); //Math.Round(AmountAfterDiscount); //Qty * CPrice;
                                                dsCart.Tables[CartTableName].Rows[i]["UOM"] = UOM;
                                                dsCart.Tables[CartTableName].Rows[i]["VariantCode"] = Variant;
                                                dsCart.Tables[CartTableName].Rows[i]["Remark"] = Remark;
                                                // Removed Roudingdue to the problem of Sales Order Line creation. - Raj Shah 24/09/2015
                                                dsCart.Tables[CartTableName].Rows[i]["CSellingPrice"] = (BillPrice);
                                                dsCart.Tables[CartTableName].Rows[i]["DiscPrice"] = (DisPrice);
                                                dsCart.Tables[CartTableName].Rows[i]["DiscPercentage"] = DisPercentage;
                                                dsCart.Tables[CartTableName].Rows[i]["ExtField_Cprice"] = CpriceDiscount;
                                                dsCart.Tables[CartTableName].Rows[i]["ExtField_SellingPrice"] = Math.Round(Qty * BillPrice);

                                                // Added below two fields by vishal for Dyes
                                                dsCart.Tables[CartTableName].Rows[i]["DyesDiscPrice"] = DyesDisAmt;
                                                dsCart.Tables[CartTableName].Rows[i]["DyesNetAmount"] = DyesNetPrice;
                                                dsCart.Tables[CartTableName].Rows[i]["BillPrice"] = BillPrice;

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateProductToCartByNoForDyes(int no, string ProductCode, string ProductName, string Variant, string UOM, decimal Price, decimal Qty, string Remark, decimal AgentPrice, decimal DisPercentage, decimal BillPrice)
        {
            try
            {
                DataSet dsCart = (DataSet)HttpContext.Current.Session[CartTableName];


                if (dsCart.Tables[CartTableName] != null)
                {
                    if (dsCart.Tables[CartTableName].Rows.Count > 0)
                    {

                        for (int i = 0; i < dsCart.Tables[CartTableName].Rows.Count; i++)
                        {
                            // Added by Raj Shah 06/10/2015
                            // Changed the code for the purpose of Veifying which item to update.
                            if (ProductCode.ToLower().Equals(dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString().ToLower()))
                            {
                                if ((dsCart.Tables[CartTableName].Rows[i]["VariantCode"].ToString().ToLower()) == "")
                                {
                                    Decimal DisPrice = (AgentPrice * DisPercentage) / 100;
                                    Decimal Amt = ((AgentPrice * Qty) * DisPercentage) / 100;
                                    Decimal AmountAfterDiscount = (AgentPrice * Qty) - Amt;
                                    Decimal CpriceDiscount = (AgentPrice - DisPrice);

                                    // Added below two fields by vishal for Dyes
                                    Decimal DyesDisAmt = (BillPrice * DisPercentage) / 100;
                                    Decimal DyesNetPrice = (BillPrice - DyesDisAmt);


                                    int inum = dsCart.Tables[CartTableName].Rows.Count;
                                    //return "Product is already exists.";

                                    dsCart.Tables[CartTableName].Rows[i]["noField"] = Convert.ToString(inum);
                                    dsCart.Tables[CartTableName].Rows[i]["ProductCode"] = ProductCode;
                                    dsCart.Tables[CartTableName].Rows[i]["ProductName"] = ProductName;
                                    dsCart.Tables[CartTableName].Rows[i]["Quantity"] = Qty;
                                    dsCart.Tables[CartTableName].Rows[i]["Rate"] = Price;
                                    dsCart.Tables[CartTableName].Rows[i]["SellingPrice"] = Math.Round(DyesNetPrice * Qty);//Math.Round(AmountAfterDiscount); //Qty * CPrice;
                                    dsCart.Tables[CartTableName].Rows[i]["UOM"] = UOM;
                                    dsCart.Tables[CartTableName].Rows[i]["VariantCode"] = Variant;
                                    dsCart.Tables[CartTableName].Rows[i]["Remark"] = Remark;
                                    // Removed Roudingdue to the problem of Sales Order Line creation. - Raj Shah 24/09/2015
                                    dsCart.Tables[CartTableName].Rows[i]["CSellingPrice"] = (BillPrice);
                                    dsCart.Tables[CartTableName].Rows[i]["DiscPrice"] = (DisPrice);
                                    dsCart.Tables[CartTableName].Rows[i]["DiscPercentage"] = DisPercentage;
                                    dsCart.Tables[CartTableName].Rows[i]["ExtField_Cprice"] = CpriceDiscount;
                                    dsCart.Tables[CartTableName].Rows[i]["ExtField_SellingPrice"] = Math.Round(Qty * BillPrice);

                                    // Added below three fields by vishal for Dyes
                                    dsCart.Tables[CartTableName].Rows[i]["DyesDiscPrice"] = DyesDisAmt;
                                    dsCart.Tables[CartTableName].Rows[i]["DyesNetAmount"] = DyesNetPrice;
                                    dsCart.Tables[CartTableName].Rows[i]["BillPrice"] = BillPrice;


                                }
                                else
                                {
                                    if (ProductCode.ToLower().Equals(dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString().ToLower()))
                                    {
                                        // reverse for that 
                                        if ((dsCart.Tables[CartTableName].Rows[i]["VariantCode"].ToString().ToLower()) != "")
                                        {

                                            if (dsCart.Tables[CartTableName].Rows[i]["noField"].ToString().ToLower() == Convert.ToString(no))
                                            {
                                                Decimal DisPrice = (AgentPrice * DisPercentage) / 100;
                                                Decimal Amt = ((AgentPrice * Qty) * DisPercentage) / 100;
                                                Decimal AmountAfterDiscount = (AgentPrice * Qty) - Amt;
                                                Decimal CpriceDiscount = (AgentPrice - DisPrice);

                                                // Added below two fields by vishal for Dyes
                                                Decimal DyesDisAmt = (BillPrice * DisPercentage) / 100;
                                                Decimal DyesNetPrice = (BillPrice - DyesDisAmt);


                                                int inum = dsCart.Tables[CartTableName].Rows.Count;
                                                //return "Product is already exists.";

                                                dsCart.Tables[CartTableName].Rows[i]["noField"] = Convert.ToString(no);
                                                dsCart.Tables[CartTableName].Rows[i]["ProductCode"] = ProductCode;
                                                dsCart.Tables[CartTableName].Rows[i]["ProductName"] = ProductName;
                                                dsCart.Tables[CartTableName].Rows[i]["Quantity"] = Qty;
                                                dsCart.Tables[CartTableName].Rows[i]["Rate"] = Price;
                                                dsCart.Tables[CartTableName].Rows[i]["SellingPrice"] = Math.Round(DyesNetPrice * Qty);//Math.Round(AmountAfterDiscount); //Qty * CPrice;
                                                dsCart.Tables[CartTableName].Rows[i]["UOM"] = UOM;
                                                dsCart.Tables[CartTableName].Rows[i]["VariantCode"] = Variant;
                                                dsCart.Tables[CartTableName].Rows[i]["Remark"] = Remark;
                                                // Removed Roudingdue to the problem of Sales Order Line creation. - Raj Shah 24/09/2015
                                                dsCart.Tables[CartTableName].Rows[i]["CSellingPrice"] = (BillPrice);
                                                dsCart.Tables[CartTableName].Rows[i]["DiscPrice"] = (DisPrice);
                                                dsCart.Tables[CartTableName].Rows[i]["DiscPercentage"] = DisPercentage;
                                                dsCart.Tables[CartTableName].Rows[i]["ExtField_Cprice"] = CpriceDiscount;
                                                dsCart.Tables[CartTableName].Rows[i]["ExtField_SellingPrice"] = Math.Round(Qty * BillPrice);

                                                // Added below three fields by vishal for Dyes
                                                dsCart.Tables[CartTableName].Rows[i]["DyesDiscPrice"] = DyesDisAmt;
                                                dsCart.Tables[CartTableName].Rows[i]["DyesNetAmount"] = DyesNetPrice;
                                                dsCart.Tables[CartTableName].Rows[i]["BillPrice"] = BillPrice;

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetCartCount()
        {
            try
            {
                if (HttpContext.Current.Session[CartTableName] != null)
                {
                    DataSet dsCart = (DataSet)HttpContext.Current.Session[CartTableName];
                    if (dsCart.Tables[CartTableName] != null)
                    {
                        return dsCart.Tables[CartTableName].Rows.Count.ToString();
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ClearCart(HttpContext hc)
        {
            try
            {
                hc.Session[CartTableName] = null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string DeleteProductFromCart(string ProductCode, int id)
        {
            try
            {
                if (HttpContext.Current.Session[CartTableName] != null)
                {
                    DataSet dsCart = (DataSet)HttpContext.Current.Session[CartTableName];
                    if (dsCart.Tables[CartTableName] != null)
                    {
                        //foreach (DataRow dr in dsCart.Tables[CartTableName].Rows)
                        //{
                        //    if (ProductCode.ToLower().Equals(dr["ProductCode"].ToString().ToLower()))
                        //    {
                        //        dsCart.Tables[CartTableName].Rows.Remove(dr);
                        //    }
                        //}
                        for (int i = 0; i < dsCart.Tables[CartTableName].Rows.Count; i++)
                        {
                            if (ProductCode.ToLower().Equals(dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString().ToLower()))
                            {
                                if ((dsCart.Tables[CartTableName].Rows[i]["noField"].ToString().ToLower()) == Convert.ToString(id))
                                {
                                    dsCart.Tables[CartTableName].Rows.RemoveAt(i);
                                }
                            }
                        }
                        dsCart.Tables[CartTableName].AcceptChanges();
                    }
                    HttpContext.Current.Session[CartTableName] = dsCart;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string DeleteProductFromCartData(string ProductCode, int Rate)
        {
            try
            {
                if (HttpContext.Current.Session[CartTableName] != null)
                {
                    DataSet dsCart = (DataSet)HttpContext.Current.Session[CartTableName];
                    if (dsCart.Tables[CartTableName] != null)
                    {
                        for (int i = 0; i < dsCart.Tables[CartTableName].Rows.Count; i++)
                        {
                            if (ProductCode.ToLower().Equals(dsCart.Tables[CartTableName].Rows[i]["ProductCode"].ToString().ToLower()))
                            {
                                if ((dsCart.Tables[CartTableName].Rows[i]["Rate"].ToString().ToLower()) == Convert.ToString(Rate))
                                {
                                    dsCart.Tables[CartTableName].Rows.RemoveAt(i);
                                }
                            }
                            dsCart.Tables[CartTableName].AcceptChanges();
                        }
                        dsCart.Tables[CartTableName].AcceptChanges();
                    }
                    HttpContext.Current.Session[CartTableName] = dsCart;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}