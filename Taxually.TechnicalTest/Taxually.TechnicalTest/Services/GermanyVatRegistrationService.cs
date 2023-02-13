﻿using System.Xml.Serialization;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services;

/// <summary>
/// <see cref="IVatRegistrationService"/> implementation for Germany
/// </summary>
public class GermanyVatRegistrationService : IVatRegistrationService
{
    /// <inheritdoc />
    public async Task RegisterCompany(VatRegistrationRequest request)
    {
        using (var stringwriter = new StringWriter())
        {
            var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
            serializer.Serialize(stringwriter, request);
            var xml = stringwriter.ToString();
            var xmlQueueClient = new TaxuallyQueueClient();
            // Queue xml doc to be processed
            await xmlQueueClient.EnqueueAsync("vat-registration-xml", xml);
        }
    }
}