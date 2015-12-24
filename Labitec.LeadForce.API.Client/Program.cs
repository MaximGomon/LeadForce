using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Labitec.LeadForce.API.Client.ActionClientReference;
using Labitec.LeadForce.API.Client.ContactClientReference;
using Labitec.LeadForce.API.Client.InvoiceClientReference;
using Labitec.LeadForce.API.Client.ProductClientReference;
using Labitec.LeadForce.API.Client.RequirementClientReference;
using Labitec.LeadForce.API.Client.UserClientReference;

namespace Labitec.LeadForce.API.Client
{
    class Program
    {
        //private static Guid siteId = Guid.Parse("acada40f-be80-417f-a73b-dcc52de40edd");
        //private static string userName = "test2@test2.com";
        //private static string password = "test2";

        private static Guid siteId = Guid.Parse("2226d30e-baee-4f65-b6f0-b52f81c80885");
        private static string userName = "api@clinic365.ru";
        private static string password = "Dm3szxeK69";


        static void Main(string[] args)
        {
            //ContactCheckNameTest();
            ContactGetContactsTest();
            //ContactUpdateContactTest();
            //RequirementGetRequirementsTest();
            //RequirementUpdateRequirementTest();
            //UserGetUsersTest();
            //UserUpdateUserTest();
            //ProductGetProductsTest();
            //ProductUpdateProductTest();
            //InvoiceGetInvoicesTest();
            //InvoiceUpdateInvoiceTest();
            Console.ReadKey();
        
        }



        public static void ContactCheckNameTest()
        {
            var client = new ContactClient();
            var resultCheckName = client.CheckName(siteId, userName, password, "Иванов Иван Иванович");

            Console.WriteLine(resultCheckName);
        }


        public  static void RequirementGetRequirementsTest()
        {
            var client = new RequirementClient();
            var resultGetRequirements = client.GetRequirements(siteId, userName, password,
@"<LeadForceRequest>
    <Requirement>
        <Params>
            <RequirementID value=""0e824578-3366-46af-8b22-3a9881c93297""/>
        </Params>
    </Requirement>
</LeadForceRequest>");

//@"<LeadForceRequest>
//    <Requirement>
//        <Params>
//            <ResponsibleID value=""bff90d07-648c-44ba-9a36-46629e9b2280""/>
//            <Status value=""К оплате""/>
//            <CompanyID value=""55924efb-220e-4250-9442-9534d99d2016"" />
//            <RequirementType value=""Тип 2"" />
//        </Params>
//    </Requirement>
//</LeadForceRequest>"

            Console.WriteLine(resultGetRequirements);
        }


        public static void RequirementUpdateRequirementTest()
        {
            var client = new RequirementClient();
            var resultUpdateRequirement = client.UpdateRequirement(siteId, userName, password,
@"<LeadForceRequest><Requirement RequirementID=""0FC3200B-4A74-4D6A-8556-4B78EAD7837E"" CreatedAt=""2012-03-16T15:51:56.837"" Number=""2012/3"">
    <Status>К оплате</Status>
    <Responsible>Шевченко Денис</Responsible>
    <RealizationDatePlanned>2012-03-16T15:51:56.837</RealizationDatePlanned>
    <ShortDescription>Тестирование нотификации 2222</ShortDescription>
    <Source>
      <RequestType>Источник обращение 1</RequestType>
      <Company>Компания</Company>
      <Contact>Белик Алексей Викторович</Contact>
      <ProductSeriesNumber />
    </Source>
    <Classification>
      <RequirementType>Тип 2</RequirementType>
      <ServiceLevel>Уровень обслуживания 3</ServiceLevel>
    </Classification>
    <ClientReview>
      <Comment />
    </ClientReview>
    <EstimateForClient>
      <AnyProductName />
      <Quantity>0.0000</Quantity>
      <Rate>1.0000</Rate>
      <CurrencyPrice>0.0000</CurrencyPrice>
      <CurrencyAmount>0.0000</CurrencyAmount>
      <Price>0.0000</Price>
      <Amount>0.0000</Amount>
    </EstimateForClient>
    <InternalEstimate>
      <Quantity>0.0000</Quantity>
    </InternalEstimate>
    <Comments>
      <Comment CommentID=""A4B72323-92A7-4C92-A146-D114089AC4F5"" UserID=""2FE30B02-2DCC-4A45-8AD6-DE356B27A1C1"" IsOfficialAnswer=""1"">
        <Text>aaaaaaaaaaaaaffffff</Text>
      </Comment>
      <Comment CommentID=""CCB72324-92A7-4C92-A146-D114089AC4F5"" UserID=""2FE30B02-2DCC-4A45-8AD6-DE356B27A1C1"" IsOfficialAnswer=""0"">
        <Text>bbbbbfffff</Text>
      </Comment>
      <Comment CommentID=""f8d53358-47c0-4b53-8232-26e7c368e82d"">
        <Text>www</Text>
      </Comment>      
    </Comments>    
  </Requirement>
</LeadForceRequest>");

            Console.WriteLine(resultUpdateRequirement);
        }


        public static void ContactGetContactsTest()
        {
            var client = new ContactClient();
            
            var resultGetContacts = client.GetContacts(siteId, userName, password,
@"<LeadForceRequest>
	<Contact>
		<Params>			
			<LastActivityAt start=""2013-10-11"" end=""2013-10-21"" />
        </Params>
    </Contact>
</LeadForceRequest>");

            Console.WriteLine(resultGetContacts);
        }



        public static void ContactUpdateContactTest()
        {
            var client = new ContactClient();
            
            var resultUpdateContact = client.UpdateContact(siteId, userName, password,
                                                           @"<LeadForceRequest>
                <Contact ContactID=""A6A19108-27EB-11E1-BB0D-DA8C4824019B"">	
            	    <UserFullName>Белик Алексей2 Викторович</UserFullName>
            	    <Email>a.b.e.l.i.c.k.@gmail.com</Email>
            	    <Phone>5555555555</Phone>	
            	    <Status>Активна</Status>	
            	    <IsNameChecked>1</IsNameChecked>
            	    <Surname>Белик</Surname>
            	    <Name>Алексей2</Name>
            	    <Patronymic>Викторович</Patronymic>
            	    <CellularPhone>111111111</CellularPhone>
            	    <CellularPhoneStatus>0</CellularPhoneStatus>
            	    <EmailStatus>0</EmailStatus>
            	    <JobTitle>Web developer</JobTitle>
            	    <Company>dfgds fgsdf test company</Company>
            	    <Owner>Current</Owner>
            	    <BirthDate>1985-05-30T00:00:00</BirthDate>
            	    <ContactFunctionInCompany>ИТ</ContactFunctionInCompany>
            	    <ContactJobLevel>Сотрудник</ContactJobLevel>	
            	    <ContactType></ContactType>
            	    <Reffer></Reffer>
                    <AdvertisingType>Тип рекламы 1</AdvertisingType>
                    <AdvertisingCampaign>Рекламная кампания 10</AdvertisingCampaign>
                    <AdvertisingPlatform>Рекламная площадка 3</AdvertisingPlatform>
            	    <Address Address=""Украина, Центральная Украина, Кировоградская область, Кировоград"">
            		    <Country>Украина</Country>
            		    <Region>Центральная Украина</Region>
            		    <District>Кировоградская область</District>
            		    <City>Кировоград</City>
            	    </Address>
                    <Tags>API тег 1, API тег 2</Tags>
                </Contact>
            </LeadForceRequest>");

            Console.WriteLine(resultUpdateContact);
        }



        public static void ActionGetActionsTest()
        {
            var actionClient = new ActionClient();
            var resultGetActions = actionClient.GetActions(siteId, userName, password,
                                                           @"<LeadForceRequest>
            	<Action>
            		<Params>			
            			<ActivityCode value=""form1""/>
            		</Params>
            	</Action>
            </LeadForceRequest>");

            Console.WriteLine(resultGetActions); 
        }


        public static void ActionCreateActionTest()
        {
            var actionClient = new ActionClient();

            var resultCreateAction = actionClient.CreateAction(siteId, userName, password,
                                                           @"<LeadForceRequest>
            	<Action>
		            <ContactID>A6A19108-27EB-11E1-BB0D-DA8C4824019B</ContactID>
		            <ActivityType></ActivityType>
		            <ActivityCode></ActivityCode>
	            </Action>
            </LeadForceRequest>");

            Console.WriteLine(resultCreateAction); 
        }

        public static void UserGetUsersTest()
        {
            var client = new UserClient();
            var resultGetUsers = client.GetUsers(siteId, userName, password,
@"<LeadForceRequest>
	<User>
		<Params>			
			<AccessProfile value=""Demo"" />
        </Params>
    </User>
</LeadForceRequest>");

            Console.WriteLine(resultGetUsers);
        }

        public static void UserUpdateUserTest()
        {
            var client = new UserClient();

            var resultUpdateUser = client.UpdateUser(siteId, userName, password,
                                                           @"<LeadForceRequest>
                <User UserID=""1266ecf9-10e0-4699-9913-a34ccfd3dea7"" ContactID=""A6A19108-27EB-11E1-BB0D-DA8C4824019B"" Login=""apitest@localhost.com"" Password=""1"" IsActive=""1"" AccessLevelID=""1"" AccessProfile=""Demo""/>  
            </LeadForceRequest>");

            Console.WriteLine(resultUpdateUser);
        }

        public static void ProductGetProductsTest()
        {
            var client = new ProductClient();
            var resultGetProducts = client.GetProducts(siteId, userName, password,
@"<LeadForceRequest>
	<Product>
		<Params>			
			<Brand value=""ElfNet"" />
        </Params>
    </Product>
</LeadForceRequest>");

            Console.WriteLine(resultGetProducts);
        }

        public static void ProductUpdateProductTest()
        {
            var client = new ProductClient();

            var resultUpdateProduct = client.UpdateProduct(siteId, userName, password,
                                                           @"<LeadForceRequest>
                <Product ProductID=""ED149BEE-9000-415D-ABCB-446CBB255075""><Title>Для мега</Title><SKU></SKU><Status>Текущий</Status><Category>ElfNet</Category><Brand>ElfNet</Brand><Type>Услуга</Type><Unit>Штука</Unit><Price>100.0000</Price><WholesalePrice>0.0000</WholesalePrice><SupplierSKU>##2222</SupplierSKU><Country>Барбадос</Country><Description>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent
condimentum velit ut sem semper interdum. Nunc id dui nec orci volutpat
pulvinar. Quisque in magna nibh. Maecenas eu nulla dolor, vitae
tristique enim. Nulla fermentum neque vitae ante imperdiet et vulputate
magna tempus. Nunc porta molestie accumsan. Fusce ut felis at turpis
bibendum vestibulum in quis risus.</Description></Product>
            </LeadForceRequest>");

            Console.WriteLine(resultUpdateProduct);
        }

        public static void InvoiceGetInvoicesTest()
        {
            var client = new InvoiceClient();
            var resultGetInvoices = client.GetInvoices(siteId, userName, password,
@"<LeadForceRequest>
	<Invoice>
		<Params>
            <InvoiceID value=""5470b853-1ab1-41d1-b349-30d7d62b9af5""/>
        </Params>
    </Invoice>
</LeadForceRequest>");

            Console.WriteLine(resultGetInvoices);
        }

        public static void InvoiceUpdateInvoiceTest()
        {
            var client = new InvoiceClient();
            var resultGetInvoices = client.UpdateInvoice(siteId, userName, password,
@"<LeadForceRequest>
	<Invoice InvoiceID=""5570B813-1AB1-41D1-B349-30D7D62B9AF5"" CreatedAt=""2012-10-12T00:00:00"" Number=""2012/2"">                         
        <Status>Не выставлен</Status>
        <Type>Тип счета 1</Type>
        <Note></Note>
        <Buyer>
            <Company>Компания</Company>
            <LegalAccount>Юр лицо 1</LegalAccount>
            <Contact>Белик Алексей Викторович</Contact>
        </Buyer>
        <Executor>
            <Company>Компания</Company>
            <LegalAccount>Юр лицо 1</LegalAccount>
            <Contact>Белик Алексей Викторович</Contact>
        </Executor>
        <Finance>
            <Amount>405.0000</Amount>
            <Paid>0.0000</Paid>
            <IsExistBuyerComplaint>1</IsExistBuyerComplaint>
            <IsPaymentDateFixedByContract>1</IsPaymentDateFixedByContract>
        </Finance>
        <InvoiceProducts>
            <InvoiceProduct InvoiceProductID=""78D22889-9EF2-448F-A896-488661102554"">
                <Product>Для мега</Product>
                <AnyProductName>вапва прв апвр прав пр</AnyProductName>
                <SerialNumber>345345345234</SerialNumber>
                <CostInOrder>
                    <PriceList>Прайс-лист 2</PriceList>
                    <Quantity>5.0000</Quantity>
                    <Unit>Штука</Unit>
                    <Currency>руб.</Currency>
                    <Rate>1.0000</Rate>
                    <CurrencyPrice>90.0000</CurrencyPrice>
                    <CurrencyAmount>450.0000</CurrencyAmount>
                    <Price>90.0000</Price>
                    <Amount>450.0000</Amount>
                </CostInOrder>
                <Discount>
                    <PriceList>Прайс-лист 2</PriceList>
                    <Discount>10.0000</Discount>
                    <CurrencyDiscountAmount>45.0000</CurrencyDiscountAmount>
                    <DiscountAmount>45.0000</DiscountAmount>
                </Discount>
                <Total>
                    <CurrencyTotalAmount>405.0000</CurrencyTotalAmount>
                    <TotalAmount>405.0000</TotalAmount>
                </Total>
            </InvoiceProduct>
        </InvoiceProducts>
    </Invoice>
</LeadForceRequest>");

            Console.WriteLine(resultGetInvoices);
        }        
    }
}
