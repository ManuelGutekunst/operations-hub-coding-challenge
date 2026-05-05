namespace OperationsHub.Api.Features.Assets;

public sealed record BatchUpdateAssetStatusesResponse(
    IReadOnlyCollection<string> UpdatedAssetCodes,
    IReadOnlyCollection<AssetBatchUpdateFailure> Failed);

public sealed record AssetBatchUpdateFailure(string AssetCode, string Reason);
