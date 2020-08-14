/// https://kafka.apache.org/protocol#protocol_error_codes
namespace KafkaRaw.Protocols
{
    public enum ErrorCodes : short
    {
        /// <summary>Retriable = False; Description = The server experienced an unexpected error when processing the request.</summary>
        UNKNOWN_SERVER_ERROR = -1,

        /// <summary>Retriable = False; Description = </summary>
        NONE = 0,

        /// <summary>Retriable = False; Description = The requested offset is not within the range of offsets maintained by the server.</summary>
        OFFSET_OUT_OF_RANGE = 1,

        /// <summary>Retriable = True; Description = This message has failed its CRC checksum, exceeds the valid size, has a null key for a compacted topic, or is otherwise corrupt.</summary>
        CORRUPT_MESSAGE = 2,

        /// <summary>Retriable = True; Description = This server does not host this topic-partition.</summary>
        UNKNOWN_TOPIC_OR_PARTITION = 3,

        /// <summary>Retriable = False; Description = The requested fetch size is invalid.</summary>
        INVALID_FETCH_SIZE = 4,

        /// <summary>Retriable = True; Description = There is no leader for this topic-partition as we are in the middle of a leadership election.</summary>
        LEADER_NOT_AVAILABLE = 5,

        /// <summary>Retriable = True; Description = For requests intended only for the leader, this error indicates that the broker is not the current leader. For requests intended for any replica, this error indicates that the broker is not a replica of the topic partition.</summary>
        NOT_LEADER_OR_FOLLOWER = 6,

        /// <summary>Retriable = True; Description = The request timed out.</summary>
        REQUEST_TIMED_OUT = 7,

        /// <summary>Retriable = False; Description = The broker is not available.</summary>
        BROKER_NOT_AVAILABLE = 8,

        /// <summary>Retriable = True; Description = The replica is not available for the requested topic-partition. Produce/Fetch requests and other requests intended only for the leader or follower return NOT_LEADER_OR_FOLLOWER if the broker is not a replica of the topic-partition.</summary>
        REPLICA_NOT_AVAILABLE = 9,

        /// <summary>Retriable = False; Description = The request included a message larger than the max message size the server will accept.</summary>
        MESSAGE_TOO_LARGE = 10,

        /// <summary>Retriable = False; Description = The controller moved to another broker.</summary>
        STALE_CONTROLLER_EPOCH = 11,

        /// <summary>Retriable = False; Description = The metadata field of the offset request was too large.</summary>
        OFFSET_METADATA_TOO_LARGE = 12,

        /// <summary>Retriable = True; Description = The server disconnected before a response was received.</summary>
        NETWORK_EXCEPTION = 13,

        /// <summary>Retriable = True; Description = The coordinator is loading and hence can't process requests.</summary>
        COORDINATOR_LOAD_IN_PROGRESS = 14,

        /// <summary>Retriable = True; Description = The coordinator is not available.</summary>
        COORDINATOR_NOT_AVAILABLE = 15,

        /// <summary>Retriable = True; Description = This is not the correct coordinator.</summary>
        NOT_COORDINATOR = 16,

        /// <summary>Retriable = False; Description = The request attempted to perform an operation on an invalid topic.</summary>
        INVALID_TOPIC_EXCEPTION = 17,

        /// <summary>Retriable = False; Description = The request included message batch larger than the configured segment size on the server.</summary>
        RECORD_LIST_TOO_LARGE = 18,

        /// <summary>Retriable = True; Description = Messages are rejected since there are fewer in-sync replicas than required.</summary>
        NOT_ENOUGH_REPLICAS = 19,

        /// <summary>Retriable = True; Description = Messages are written to the log, but to fewer in-sync replicas than required.</summary>
        NOT_ENOUGH_REPLICAS_AFTER_APPEND = 20,

        /// <summary>Retriable = False; Description = Produce request specified an invalid value for required acks.</summary>
        INVALID_REQUIRED_ACKS = 21,

        /// <summary>Retriable = False; Description = Specified group generation id is not valid.</summary>
        ILLEGAL_GENERATION = 22,

        /// <summary>Retriable = False; Description = The group member's supported protocols are incompatible with those of existing members or first group member tried to join with empty protocol type or empty protocol list.</summary>
        INCONSISTENT_GROUP_PROTOCOL = 23,

        /// <summary>Retriable = False; Description = The configured groupId is invalid.</summary>
        INVALID_GROUP_ID = 24,

        /// <summary>Retriable = False; Description = The coordinator is not aware of this member.</summary>
        UNKNOWN_MEMBER_ID = 25,

        /// <summary>Retriable = False; Description = The session timeout is not within the range allowed by the broker (as configured by group.min.session.timeout.ms and group.max.session.timeout.ms).</summary>
        INVALID_SESSION_TIMEOUT = 26,

        /// <summary>Retriable = False; Description = The group is rebalancing, so a rejoin is needed.</summary>
        REBALANCE_IN_PROGRESS = 27,

        /// <summary>Retriable = False; Description = The committing offset data size is not valid.</summary>
        INVALID_COMMIT_OFFSET_SIZE = 28,

        /// <summary>Retriable = False; Description = Topic authorization failed.</summary>
        TOPIC_AUTHORIZATION_FAILED = 29,

        /// <summary>Retriable = False; Description = Group authorization failed.</summary>
        GROUP_AUTHORIZATION_FAILED = 30,

        /// <summary>Retriable = False; Description = Cluster authorization failed.</summary>
        CLUSTER_AUTHORIZATION_FAILED = 31,

        /// <summary>Retriable = False; Description = The timestamp of the message is out of acceptable range.</summary>
        INVALID_TIMESTAMP = 32,

        /// <summary>Retriable = False; Description = The broker does not support the requested SASL mechanism.</summary>
        UNSUPPORTED_SASL_MECHANISM = 33,

        /// <summary>Retriable = False; Description = Request is not valid given the current SASL state.</summary>
        ILLEGAL_SASL_STATE = 34,

        /// <summary>Retriable = False; Description = The version of API is not supported.</summary>
        UNSUPPORTED_VERSION = 35,

        /// <summary>Retriable = False; Description = Topic with this name already exists.</summary>
        TOPIC_ALREADY_EXISTS = 36,

        /// <summary>Retriable = False; Description = Number of partitions is below 1.</summary>
        INVALID_PARTITIONS = 37,

        /// <summary>Retriable = False; Description = Replication factor is below 1 or larger than the number of available brokers.</summary>
        INVALID_REPLICATION_FACTOR = 38,

        /// <summary>Retriable = False; Description = Replica assignment is invalid.</summary>
        INVALID_REPLICA_ASSIGNMENT = 39,

        /// <summary>Retriable = False; Description = Configuration is invalid.</summary>
        INVALID_CONFIG = 40,

        /// <summary>Retriable = True; Description = This is not the correct controller for this cluster.</summary>
        NOT_CONTROLLER = 41,

        /// <summary>Retriable = False; Description = This most likely occurs because of a request being malformed by the client library or the message was sent to an incompatible broker. See the broker logs for more details.</summary>
        INVALID_REQUEST = 42,

        /// <summary>Retriable = False; Description = The message format version on the broker does not support the request.</summary>
        UNSUPPORTED_FOR_MESSAGE_FORMAT = 43,

        /// <summary>Retriable = False; Description = Request parameters do not satisfy the configured policy.</summary>
        POLICY_VIOLATION = 44,

        /// <summary>Retriable = False; Description = The broker received an out of order sequence number.</summary>
        OUT_OF_ORDER_SEQUENCE_NUMBER = 45,

        /// <summary>Retriable = False; Description = The broker received a duplicate sequence number.</summary>
        DUPLICATE_SEQUENCE_NUMBER = 46,

        /// <summary>Retriable = False; Description = Producer attempted an operation with an old epoch. Either there is a newer producer with the same transactionalId, or the producer's transaction has been expired by the broker.</summary>
        INVALID_PRODUCER_EPOCH = 47,

        /// <summary>Retriable = False; Description = The producer attempted a transactional operation in an invalid state.</summary>
        INVALID_TXN_STATE = 48,

        /// <summary>Retriable = False; Description = The producer attempted to use a producer id which is not currently assigned to its transactional id.</summary>
        INVALID_PRODUCER_ID_MAPPING = 49,

        /// <summary>Retriable = False; Description = The transaction timeout is larger than the maximum value allowed by the broker (as configured by transaction.max.timeout.ms).</summary>
        INVALID_TRANSACTION_TIMEOUT = 50,

        /// <summary>Retriable = False; Description = The producer attempted to update a transaction while another concurrent operation on the same transaction was ongoing.</summary>
        CONCURRENT_TRANSACTIONS = 51,

        /// <summary>Retriable = False; Description = Indicates that the transaction coordinator sending a WriteTxnMarker is no longer the current coordinator for a given producer.</summary>
        TRANSACTION_COORDINATOR_FENCED = 52,

        /// <summary>Retriable = False; Description = Transactional Id authorization failed.</summary>
        TRANSACTIONAL_ID_AUTHORIZATION_FAILED = 53,

        /// <summary>Retriable = False; Description = Security features are disabled.</summary>
        SECURITY_DISABLED = 54,

        /// <summary>Retriable = False; Description = The broker did not attempt to execute this operation. This may happen for batched RPCs where some operations in the batch failed, causing the broker to respond without trying the rest.</summary>
        OPERATION_NOT_ATTEMPTED = 55,

        /// <summary>Retriable = True; Description = Disk error when trying to access log file on the disk.</summary>
        KAFKA_STORAGE_ERROR = 56,

        /// <summary>Retriable = False; Description = The user-specified log directory is not found in the broker config.</summary>
        LOG_DIR_NOT_FOUND = 57,
        /// <summary>Retriable = False; Description = SASL Authentication failed.</summary>
        SASL_AUTHENTICATION_FAILED = 58,

        /// <summary>Retriable = False; Description = This exception is raised by the broker if it could not locate the producer metadata associated with the producerId in question. This could happen if, for instance, the producer's records were deleted because their retention time had elapsed. Once the last records of the producerId are removed, the producer's metadata is removed from the broker, and future appends by the producer will return this exception.</summary>
        UNKNOWN_PRODUCER_ID = 59,

        /// <summary>Retriable = False; Description = A partition reassignment is in progress.</summary>
        REASSIGNMENT_IN_PROGRESS = 60,

        /// <summary>Retriable = False; Description = Delegation Token feature is not enabled.</summary>
        DELEGATION_TOKEN_AUTH_DISABLED = 61,

        /// <summary>Retriable = False; Description = Delegation Token is not found on server.</summary>
        DELEGATION_TOKEN_NOT_FOUND = 62,

        /// <summary>Retriable = False; Description = Specified Principal is not valid Owner/Renewer.</summary>
        DELEGATION_TOKEN_OWNER_MISMATCH = 63,

        /// <summary>Retriable = False; Description = Delegation Token requests are not allowed on PLAINTEXT/1-way SSL channels and on delegation token authenticated channels.</summary>
        DELEGATION_TOKEN_REQUEST_NOT_ALLOWED = 64,

        /// <summary>Retriable = False; Description = Delegation Token authorization failed.</summary>
        DELEGATION_TOKEN_AUTHORIZATION_FAILED = 65,

        /// <summary>Retriable = False; Description = Delegation Token is expired.</summary>
        DELEGATION_TOKEN_EXPIRED = 66,

        /// <summary>Retriable = False; Description = Supplied principalType is not supported.</summary>
        INVALID_PRINCIPAL_TYPE = 67,

        /// <summary>Retriable = False; Description = The group is not empty.</summary>
        NON_EMPTY_GROUP = 68,

        /// <summary>Retriable = False; Description = The group id does not exist.</summary>
        GROUP_ID_NOT_FOUND = 69,

        /// <summary>Retriable = True; Description = The fetch session ID was not found.</summary>
        FETCH_SESSION_ID_NOT_FOUND = 70,

        /// <summary>Retriable = True; Description = The fetch session epoch is invalid.</summary>
        INVALID_FETCH_SESSION_EPOCH = 71,

        /// <summary>Retriable = True; Description = There is no listener on the leader broker that matches the listener on which metadata request was processed.</summary>
        LISTENER_NOT_FOUND = 72,

        /// <summary>Retriable = False; Description = Topic deletion is disabled.</summary>
        TOPIC_DELETION_DISABLED = 73,

        /// <summary>Retriable = True; Description = The leader epoch in the request is older than the epoch on the broker.</summary>
        FENCED_LEADER_EPOCH = 74,

        /// <summary>Retriable = True; Description = The leader epoch in the request is newer than the epoch on the broker.</summary>
        UNKNOWN_LEADER_EPOCH = 75,

        /// <summary>Retriable = False; Description = The requesting client does not support the compression type of given partition.</summary>
        UNSUPPORTED_COMPRESSION_TYPE = 76,

        /// <summary>Retriable = False; Description = Broker epoch has changed.</summary>
        STALE_BROKER_EPOCH = 77,

        /// <summary>Retriable = True; Description = The leader high watermark has not caught up from a recent leader election so the offsets cannot be guaranteed to be monotonically increasing.</summary>
        OFFSET_NOT_AVAILABLE = 78,

        /// <summary>Retriable = False; Description = The group member needs to have a valid member id before actually entering a consumer group.</summary>
        MEMBER_ID_REQUIRED = 79,
        /// <summary>Retriable = True; Description = The preferred leader was not available.</summary>
        PREFERRED_LEADER_NOT_AVAILABLE = 80,

        /// <summary>Retriable = False; Description = The consumer group has reached its max size.</summary>
        GROUP_MAX_SIZE_REACHED = 81,

        /// <summary>Retriable = False; Description = The broker rejected this static consumer since another consumer with the same group.instance.id has registered with a different member.id.</summary>
        FENCED_INSTANCE_ID = 82,

        /// <summary>Retriable = True; Description = Eligible topic partition leaders are not available.</summary>
        ELIGIBLE_LEADERS_NOT_AVAILABLE = 83,

        /// <summary>Retriable = True; Description = Leader election not needed for topic partition.</summary>
        ELECTION_NOT_NEEDED = 84,

        /// <summary>Retriable = False; Description = No partition reassignment is in progress.</summary>
        NO_REASSIGNMENT_IN_PROGRESS = 85,

        /// <summary>Retriable = False; Description = Deleting offsets of a topic is forbidden while the consumer group is actively subscribed to it.</summary>
        GROUP_SUBSCRIBED_TO_TOPIC = 86,

        /// <summary>Retriable = False; Description = This record has failed the validation on broker and hence will be rejected.</summary>
        INVALID_RECORD = 87,

        /// <summary>Retriable = True; Description = There are unstable offsets that need to be cleared.</summary>
        UNSTABLE_OFFSET_COMMIT = 88
    }
}