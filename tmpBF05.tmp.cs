public async Task<Result<List<Product>>> List()
{
    var result = new Result<List<Product>> { Value = new List<Product>() };

    try
    {
        var products = await _httpClient.GetFromJsonAsync<List<Product>>("TodoLists");
        result.Value = products ?? new List<Product>();
    }
    catch (HttpRequestException ex)
    {
        if (ex.HttpRequestError == HttpRequestError.ConnectionError)
        {
            result.Error = "Ei saa serveriga ühendust. Palun proovi hiljem uuesti.";
        }
        else
        {
            result.Error = ex.Message;
        }
    }
    catch (Exception ex)
    {
        result.Error = ex.Message;
    }

    return result;
}
