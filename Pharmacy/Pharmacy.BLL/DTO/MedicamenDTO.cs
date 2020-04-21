using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.BLL.DTO
{
    public class MedicamenDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Производитель лекарства
        /// </summary>
        public int ManufacturerId { get; set; }
        /// <summary>
        ///  Код АТХ
        /// </summary>
        public string CodeATC { get; set; }
        /// <summary>
        /// Форма препарата (Мазь, сироп, порошок)
        /// </summary>
        public string ProductForm { get; set; }
        /// <summary>
        /// Состав
        /// </summary>
        public string Composition { get; set; }
        /// <summary>
        /// Передозировка
        /// </summary>
        public string Overdose { get; set; }
        /// <summary>
        /// Побочные еффекты
        /// </summary>
        public string SideEffect { get; set; }
        /// <summary>
        /// Противопоказания
        /// </summary>
        public string Contraindication { get; set; }
        /// <summary>
        /// Метод применения
        /// </summary>
        public string MethodOfApplication { get; set; }
        /// <summary>
        /// Условия отпуска из аптек
        /// </summary>
        public string PharmacyVacationTerms { get; set; }
        /// <summary>
        /// Срок годности и условия хранения
        /// </summary>
        public string TermsAndConditionsOfStorage { get; set; }
    }
}
