namespace OperationsHub.Api.Features.Assets;

public sealed record BatchUpdateAssetStatusesRequest(IReadOnlyCollection<AssetStatusUpdateItem> Updates);

public sealed record AssetStatusUpdateItem(string AssetCode, string Status);
